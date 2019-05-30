#region Liceense
//  Distrubted Under the GNU Public License version 3 (GPLv3)
// ========================================
// 
// Triangles Vs Circles
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//  
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  The full license is also included in the root folder.
// ========================================
// 
// Contacts:
//   Robert Long - rbtLong@live.com
//   Richard Vong - vongr@outlook.com
//   Fausto Sihite - fsihite@uci.edu
#endregion

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Levels;
using TrianglesVCircles.Core.Projectiles.EnemyProjectiles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        private ObservableImmutableList<BaseEnemy> _enemies
            = new ObservableImmutableList<BaseEnemy>();
        private BaseLevel _currentLevel;

        public event EventHandler<BaseEnemy> EnemyRemoved = delegate { };
        public event EventHandler<BaseEnemy> EnemyAdded = delegate { };
        public event EventHandler<BaseLevel> NewLevel = delegate { };
        public event EventHandler<Tuple<BaseLevel, string>> LevelMessage = delegate { };
        public event EventHandler<Tuple<BaseLevel, string>> LevelHint = delegate { }; 

        public BaseLevel CurrentLevel
        {
            get { return _currentLevel; }
            set
            {
                if (value == _currentLevel) return;
                _currentLevel = value;
                OnPropertyChanged();
            }
        }

        public ObservableImmutableList<BaseEnemy> Enemies
        {
            get { return _enemies; }
            set
            {
                if (Equals(value, _enemies)) return;
                _enemies = value;
                OnPropertyChanged();
            }
        }

        private void initLevels()
        {
            Enemies.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var e = Enemies.LastOrDefault();

                    e.Died += (o, s) => Task.Run(() => enemyDestroyedPoint(e));
                    e.Attack.Hit += (o, p) => Task.Run(() => ShipHit(this, p.Damage));
                    e.Attack.Added += (o, p) => EnemyProjectiles.DoAdd(itm => p as BaseEnemyProjectile);
                    //e.Attack.Removed += (o, p) => EnemyProjectiles.DoRemove(itm => p as BaseEnemyProjectile);

                    EnemyAdded(this, args.NewItems[0] as BaseEnemy);
                }
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    EnemyRemoved(this, args.OldItems[0] as BaseEnemy);
                }
            };
        }


        public void LoadLevel(int level)
        {
            synch.Post(d =>
            {
                Enemies.Clear();
                _shipAffects.Clear();
                //_scoring.Scores.Clear();

                _currentLevel = levelResolver(level);

                if (_currentLevel != null)
                {
                    _currentLevel.StageMessage
                        += (o, msg) => LevelMessage(this, Tuple.Create(_currentLevel, msg));
                    _currentLevel.LevelComplete += _currentLevel_LevelComplete;
                    _currentLevel.IncarnationRestore += (sender, i) => Incarnation += i;
                    _currentLevel.HealthRestore += (s, e) =>
                    {
                        if (_ship.Health + e >= 100)
                            _ship.Health = 100;
                        else _ship.Health += e;
                    };
                    _currentLevel.Begin();
                    NewLevel(this, _currentLevel);
                }
            }, null);


        }

                private BaseLevel levelResolver(int level)
                {
                    if (level == 0) return new Level0(ref _enemies, _ship);
                    if (level == 1) return new Level1(ref _enemies, _ship);
                    if (level == 2) return new Level2(ref _enemies, _ship);
                    return null;
                }

            void _currentLevel_LevelComplete(object sender, EventArgs e)
            {
                var prevLvl = sender as BaseLevel;
                if (prevLvl.LevelNumber == 0) LoadLevel(1);
                else if (prevLvl.LevelNumber == 1) LoadLevel(2);
                else if (prevLvl.LevelNumber == 2) LoadLevel(3);
                else if (prevLvl.LevelNumber == 3) LoadLevel(4);
            }


        public void SkipLevel()
        {
            if(_currentLevel != null)
                _currentLevel.SkipLevel();
        }
    }
}
