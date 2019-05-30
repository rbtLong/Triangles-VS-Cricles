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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies;

namespace TrianglesVCircles.Core.Animation.RandomMovements
{
    public class RandomDestination : INotifyPropertyChanged
    {
        private double _xRandom;
        private double _yRandom;
        private bool _isMoving;
        private Rect _bounds;
        private Point _origin;
        private IMoveStrategy _strategy;

        public IMoveStrategy Strategy
        {
            get { return _strategy; }
            set
            {
                if (Equals(value, _strategy)) return;
                _strategy = value;
                OnPropertyChanged();
            }
        }

        public Point Delta
        {
            get
            {
                return new Point(
                    _origin.X - _xRandom,
                    _origin.Y - _yRandom);
            }
        }

        public Rect Bounds
        {
            get { return _bounds; }
        }

        public double XDelta
        {
            get { return _xRandom - _origin.X; }
        }

        public double YDelta
        {
            get { return _yRandom - _origin.Y; }
        }

        public Point Origin
        {
            get { return _origin; }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                if (value.Equals(_isMoving)) return;
                _isMoving = value;
                OnPropertyChanged();
            }
        }

        public double XRandom
        {
            get { return _xRandom; }
            set
            {
                if (value.Equals(_xRandom)) return;
                _xRandom = value;
                OnPropertyChanged();
            }
        }

        public double YRandom
        {
            get { return _yRandom; }
            set
            {
                if (value.Equals(_yRandom)) return;
                _yRandom = value;
                OnPropertyChanged();
            }
        }

        public RandomDestination(Point origin, Rect bound, IMoveStrategy stratey)
        {
            _strategy = stratey;
            _origin = origin;
            _bounds = bound;
            NextPoint(_origin);
        }

        public void NextPoint(Point origin)
        {
            if (!IsMoving) return;
            _origin = origin;
            XRandom = _strategy.GetNextX(_origin.X, new Point(_bounds.Left, _bounds.Right));
            YRandom = _strategy.GetNextY(_origin.Y, new Point(_bounds.Top, _bounds.Bottom));

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
