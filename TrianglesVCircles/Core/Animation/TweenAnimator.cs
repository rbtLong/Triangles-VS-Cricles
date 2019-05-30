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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Animation.Tween;
using TrianglesVCircles.Core.Animation.Tween.Easing;

namespace TrianglesVCircles.Core.Animation
{
    public class TweenAnimator : INotifyPropertyChanged, IDisposable
    {
        private readonly Timer _refresher = new Timer(25);
        private IEasing _ease = new Linear();
        private IMovable _item;
        private TimeSpan _duration;
        private readonly Stopwatch _watch = new Stopwatch();
        private double _speed = 3.5;

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

        public event EventHandler<IMovable> Completed = delegate { };
        public event EventHandler<IMovable> Begun = delegate { };
        public event EventHandler<IMovable> Stopped = delegate { };
        public event EventHandler<IMovable> Continued = delegate { };
        public event EventHandler<IMovable> Paused = delegate { };

        public double Speed
        {
            get { return _speed; }
            set
            {
                if (value.Equals(_speed)) return;
                _speed = value;
                OnPropertyChanged();
            }
        }

        public bool IsComplete
        {
            get
            {
                return _watch.Elapsed > _duration
                    || _item.InsideTarget()
                    || Item.Expired;
            }
        }

        public Point Delta
        {
            get
            {
                var xDelta = _item.Destination.X - _item.Source.XPosition;
                var yDelta = _item.Destination.Y - _item.Source.YPosition;
                if (_item.IsLinear)
                    return new Point(xDelta, yDelta);

                return new Point(
                    _item.Curve.GetX(xDelta),
                    _item.Curve.GetY(yDelta));
            }
        }

        public IMovable Item
        {
            get { return _item; }
            private set
            {
                if (Equals(value, _item)) return;
                _item = value;
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

        public TweenAnimator(IMovable item, TimeSpan duration)
        {
            _item = item;
            _duration = duration;
            _refresher.Elapsed += _refresher_Elapsed;
            _refresher.Enabled = true;

        }

            void _refresher_Elapsed(object sender, ElapsedEventArgs e)
            {
                if (IsComplete)
                {
                    _refresher.Enabled = false;
                    if (!_item.Expired)
                        _item.Expired = true;
                    Completed(this, _item);
                    return;
                }

                animate();
            }

                private void animate()
                {
                    var lapsed = TimeSpan.FromMilliseconds(
                        _watch.Elapsed.TotalMilliseconds*(1/Speed));
                    _item.XPosition = _item.XPosition
                        .AnimateOut(Delta.X, lapsed, _duration, _ease);
                    _item.YPosition = _item.YPosition
                        .AnimateOut(Delta.Y, lapsed, _duration, _ease);
                }

        public void Begin()
        {
            Begun(this, _item);
            _refresher.Enabled = true;
            _watch.Start();
        }

        public void Pause()
        {
            _refresher.Enabled = false;
            _watch.Stop();
            Paused(this, _item);
        }

        public void Stop()
        {
            _refresher.Stop();
            _watch.Stop();
            _watch.Reset();
            Stopped(this, _item);
        }

        public void Continue()
        {
            _refresher.Enabled = true;
            _watch.Start();            
            Continued(this, _item);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _refresher.Stop();
            _watch.Stop();
            _watch.Reset();
            _refresher.Dispose();
        }
    }
}
