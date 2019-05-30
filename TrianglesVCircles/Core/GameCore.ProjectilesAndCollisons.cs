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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TrianglesVCircles.Core.Affects;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Projectiles.EnemyProjectiles;
using TrianglesVCircles.Core.Projectiles.PlayerProjectiles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        readonly ObservableImmutableList<MissileProjectile> _playerProjectiles
            = new ObservableImmutableList<MissileProjectile>();
        readonly ObservableImmutableList<BaseEnemyProjectile> _enemyProjectiles 
            = new ObservableImmutableList<BaseEnemyProjectile>();
        private bool _enabled;

        public ObservableImmutableList<MissileProjectile> PlayerProjectiles
        {
            get { return _playerProjectiles; }
        }

        public ObservableImmutableList<BaseEnemyProjectile> EnemyProjectiles
        {
            get { return _enemyProjectiles; }
        }

        public void initProjectiles()
        {
            Controls.OnKeyDown += UserKeyInput_Projectiles;
        }

            private void UserKeyInput_Projectiles(object sender, KeyEventArgs arg)
            {
                if (arg.Key == Key.Up
                    || arg.Key == Key.Right
                    || arg.Key == Key.Down
                    || arg.Key == Key.Left
                    || arg.Key == Key.LeftAlt
                    || arg.Key == Key.RightAlt
                    || arg.Key == Key.LeftCtrl
                    || arg.Key == Key.RightCtrl
                    || arg.Key == Key.Escape
                    || arg.Key == Key.LeftAlt
                    || arg.Key == Key.LeftShift
                    || arg.Key == Key.RightShift)
                    return;

                if (State == GameStates.Playing)
                {
                    var foe = _enemies.FirstOrDefault(o => 
                        o.Life.Any()
                        && !o.Invulnerable
                        && enemyToKeyComparator(arg.Key, o.Life));

                    if (!ReferenceEquals(null, foe)
                        && !_shipAffects.Any(o => o is WrongButtonAffect))
                        AddPlayerProjectile(foe);
                    
                    if(ReferenceEquals(null, foe))
                        wrongButtonPenalty();
                }
            }

            private bool enemyToKeyComparator(Key key, List<string> list)
            {
                var inp = key.ToString();
                var digitFilter = new Regex("^D[\\d]");
                var numPadFilter = new Regex("^NumPad[\\d]");

                if (digitFilter.Match(inp).Success)
                    inp = inp.Replace("D", "");

                if (numPadFilter.Match(inp).Success)
                    inp = inp.Replace("NumPad", "");

                if (list.Any())
                    return string.Equals(inp, list.FirstOrDefault(),
                        StringComparison.OrdinalIgnoreCase);

                return false;
            }

        public void AddPlayerProjectile(BaseEnemy target)
        {
            if (PlayerProjectiles.Any(o => 
                o is MissileProjectile
                && (o.Target == target) 
                    && o.Enabled))
                return;

            var proj = new MissileProjectile(target,
                Ship.GetTip(new Point(target.Width, target.Height)));

            proj.Hit += (sender, args) =>
            {
                var missile = sender as MissileProjectile;
                if (missile.Target is BaseEnemy)
                {
                    var enemy = missile.Target as BaseEnemy;
                    enemy.RemoveLife();
                    PlayerProjectiles.DoRemove(o => proj);
                    if (!enemy.IsAlive)
                    {
                        Timed.Exec(
                            () =>
                            {
                                if(_enemies.Contains(enemy))
                                    Enemies.DoRemove(o => enemy);
                            },
                            1500);
                    }
                }
            };

            PlayerProjectiles.DoAdd(itm => proj);
        } 



    }
}
