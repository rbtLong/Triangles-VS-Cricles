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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Ship;

namespace TrianglesVCircles.Core
{
    public class EndGameEventArg : EventArgs
    {
        public GameCore.EndGameAction Action { get; set; }

        public EndGameEventArg(GameCore.EndGameAction action)
        {
            Action = action;
        }
    }

    public partial class GameCore : INotifyPropertyChanged, IDisposable
    {
        private readonly SynchronizationContext synch;
        private readonly ShipCore _ship = new ShipCore();
        private bool _initiated;
        private int _incarnation = 3;

        private event EventHandler<int> ShipHit = delegate { };

        public enum EndGameAction
        {
            RestartFromLevel,
            RestartFromBeginning,
            CloseGameSession,
        }

        public event EventHandler Death = delegate { };
        public event EventHandler GameOver = delegate { };
        
        public ShipCore Ship { get { return _ship; } }

        public int Incarnation
        {
            get { return _incarnation; }
            set
            {
                if (value == _incarnation) return;
                _incarnation = value;
                OnPropertyChanged();
            }
        }

        public GameCore()
        {
            synch = SynchronizationContext.Current;
            init();
        }

            private void init()
            {
                if (_initiated) return;
                initAffects();
                initShipHealth();
                initScoring();
                initProjectiles();
                initMovements();
                initLevels();
                initMenuCtrl();
                _initiated = true;
            }

                private void initMenuCtrl()
                {
                    Controls.OnKeyUp += (sender, args) =>
                    {
                        if (args.Key == Key.Escape)
                        {
                            if(State == GameStates.Playing) Pause();
                            else if(State == GameStates.Paused) Continue();
                        }
                    };
                }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Dispose()
        {
            foreach (var e in _enemyProjectiles)
                e.Expired = true;

            foreach (var e in _enemies)
            {
                e.IsAlive = false;
                e.Dispose();
            }

            _enemies.Clear();
            _enemyProjectiles.Clear();
            _playerProjectiles.Clear();
            
        }
    }

}
