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
using System.Timers;
using System.Windows;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Animation.Tween;
using TrianglesVCircles.Core.Animation.Tween.Easing;
using TrianglesVCircles.Core.Projectiles.Attacks;

namespace TrianglesVCircles.Core.Animation.RandomMovements
{
    public class RandomMover : INotifyPropertyChanged
    {
        private readonly RandomDestination _randomLocation;
        private readonly Timer _refresher = new Timer(30);
        private IRandomMovable _movableTarget;
        private DateTime _initialTime;
        private Point _initialPosition;
        private bool _continuous = true;
        private IEasing _ease = new Linear();
        private TimeSpan _duration = TimeSpan.FromMilliseconds(796);
        private bool _isRunning;
        private bool _autoManageSpeed;

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value.Equals(_duration)) return;
                _duration = value;
                OnPropertyChanged();
            }
        }

        public bool AutoManageSpeed
        {
            get { return _autoManageSpeed; }
            set
            {
                if (value.Equals(_autoManageSpeed)) return;
                _autoManageSpeed = value;
                OnPropertyChanged();
            }
        }

        public IEasing Ease
        {
            get { return _ease; }
            set
            {
                if (Equals(value, _ease)) return;
                _ease = value;
                OnPropertyChanged();
            }
        }

        public bool Continuous
        {
            get { return _continuous; }
            set
            {
                if (value.Equals(_continuous)) return;
                _continuous = value;
                OnPropertyChanged();
            }
        }

        public bool IsComplete
        {
            get
            {
                return _initialTime.Add(_duration) < DateTime.Now;
            }
        }

        public IRandomMovable MovableTarget
        {
            get { return _movableTarget; }
            set
            {
                if (Equals(value, _movableTarget)) return;
                _movableTarget = value;
                OnPropertyChanged();
            }
        }

        public RandomDestination RandomLocation
        {
            get { return _randomLocation; }
        }

        public RandomMover(IRandomMovable movableTarget)
        {
            _randomLocation = movableTarget.RandomMove;
            _movableTarget = movableTarget;
            _refresher.Elapsed += refresherElapsed;
        }

            private void refresherElapsed(object sender, ElapsedEventArgs e)
            {
                if (IsComplete && _isRunning)
                {
                    _refresher.Enabled = false;
                    if (Continuous && !_refresher.Enabled)
                    {
                        initAnimation();
                        _refresher.Enabled = true;
                    }
                    return;
                }

                _movableTarget.XPosition =
                    _initialPosition.X.AnimateInOut(
                        _movableTarget.RandomMove.XDelta,
                        DateTime.Now.Subtract(_initialTime),
                        _duration,
                        _ease);

                _movableTarget.YPosition =
                    _initialPosition.Y.AnimateInOut(
                        _movableTarget.RandomMove.YDelta,
                        DateTime.Now.Subtract(_initialTime),
                        _duration,
                        _ease);
            }

            public void Begin()
            {
                initAnimation();
                _randomLocation.IsMoving = true;
                _refresher.Enabled = true;
                _isRunning = true;
            }

        private void initAnimation()
        {
            _initialTime = DateTime.Now;
            _initialPosition.X = _movableTarget.XPosition;
            _initialPosition.Y = _movableTarget.YPosition;
            _movableTarget.RandomMove.NextPoint(_initialPosition);
            if (AutoManageSpeed)
                _duration = TimeSpan.FromMilliseconds(30 *
                    _movableTarget.RandomMove.Delta.Distance()); 
        }

        public void Continue()
        {
            if(!_isRunning) Begin();
        }

        public void Pause()
        {
            _randomLocation.IsMoving = false;
            _refresher.Enabled = false;
            Continuous = true;
            _isRunning = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
