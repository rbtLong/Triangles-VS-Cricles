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
using System.Diagnostics;
using System.Threading;
using System.Timers;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Ship;
using TrianglesVCircles.Helpers;
using Timer = System.Timers.Timer;

namespace TrianglesVCircles.Core.Levels
{
    public abstract class BaseLevel : IDisposable
    {
        private readonly Stopwatch _elapsedTime = new Stopwatch();
        private readonly Timer _timer = new Timer(100);
        protected readonly SynchronizationContext _synch;
        public bool LevelEnded { get; protected set; }


        public ShipCore Ship { get; private set; }
        public ObservableImmutableList<BaseEnemy> Enemies { get; private set; }
        public event EventHandler<string> StageMessage = delegate { };
        public event EventHandler<int> HealthRestore = delegate { };
        public event EventHandler<int> IncarnationRestore = delegate { };
        public event EventHandler LevelComplete = delegate { };

        protected virtual void OnLevelComplete()
        {
            EventHandler handler = LevelComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStageMessage(string e)
        {
            EventHandler<string> handler = StageMessage;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnHealthRestore(int e)
        {
            EventHandler<int> handler = HealthRestore;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnLifeRestore(int e)
        {
            EventHandler<int> handler = IncarnationRestore;
            if (handler != null) handler(this, e);
        }

        public string StageName { get; private set; }

        public Stopwatch ElapsedTime { get { return _elapsedTime; } }

        protected Timer Timer { get { return _timer; } }

        public int LevelNumber { get; private set; }

        protected abstract void InitializeEnemies();
        protected abstract void OnEnemiesRemoved();
        protected abstract void OnEnemiesAdded();
        protected abstract void TimerElapsed();
        protected abstract string InitializeTitleName();
        protected abstract int InitializeCurrentLevel();
        public abstract void PointDeducted(double currentScore);
        public abstract void PointGained(double currentScore);


        protected BaseLevel(
            ObservableImmutableList<BaseEnemy> enemies, 
            ShipCore ship)
        {
            _synch = SynchronizationContext.Current;
            LevelNumber = InitializeCurrentLevel();
            StageName = InitializeTitleName();
            Enemies = enemies;
            Ship = ship;
            InitializeEnemies();
            Enemies.CollectionChanged += enemiesChanged;
            _timer.Elapsed += _timer_Elapsed;
        }

            void _timer_Elapsed(object sender, ElapsedEventArgs e)
            {
                TimerElapsed();
            }

            private void enemiesChanged(object sender, 
                NotifyCollectionChangedEventArgs args)
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                    _synch.Post(d => OnEnemiesAdded(), null);

                if (args.Action == NotifyCollectionChangedAction.Remove)
                    _synch.Post(d => OnEnemiesRemoved(), null);
            }

        public void Begin()
        {
            _elapsedTime.Start();
            _timer.Start();
        }

        public void Pause()
        {
            foreach (var e in Enemies)
            {
                e.RandomMover.Pause();
                e.Attack.Pause();
            }
            _elapsedTime.Stop();
            _timer.Stop();
        }

        public void Continue()
        {
            foreach (var e in Enemies)
            {
                e.RandomMover.Continue();
                e.Attack.Continue();
            }
            _elapsedTime.Start();
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Dispose();

        }

        protected bool Lapsed(int secs)
        {
            return ElapsedTime.Elapsed >= TimeSpan.FromSeconds(secs)
                   && ElapsedTime.Elapsed <= TimeSpan.FromSeconds(secs + 1);
        }

        protected void AddEnemy(BaseEnemy enemy)
        {
            if(!LevelEnded)
                while (!Enemies.DoAdd(o => enemy)) ;
        }

        public virtual void SkipLevel()
        {
            LevelEnded = true;
            foreach (var e in Enemies)
            {
                e.Enabled = false;
                e.Attack.Stop();
                e.Dispose();
            }
            Enemies.Clear();
            OnLevelComplete();
        }
    }
}