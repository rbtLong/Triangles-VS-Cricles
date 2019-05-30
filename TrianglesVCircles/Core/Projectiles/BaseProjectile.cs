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
using System.Windows;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Projectiles.Attacks;

namespace TrianglesVCircles.Core.Projectiles
{
    public abstract class BaseProjectile : INotifyPropertyChanged, IProjectile
    {
        private int _damage = 1;
        private double _yPosition;
        private double _xPosition;
        private double _speed;
        private double _rotation;
        private bool _enabled = true;
        private double _height = 5;
        private double _width = 5;
        private bool _attackable;
        private IAttackable _target;
        private bool _expired;
        private Point _destination;

        public event EventHandler OnExpired = delegate { }; 

        public abstract ProjectileTypes Purpose { get; }
        public abstract void Update();

        public IAttackable Target
        {
            get { return _target; }
            set
            {
                if (Equals(value, _target)) return;
                _target = value;
                _destination = new Point(
                    _target.Center.X, 
                    _target.Center.Y);
                OnPropertyChanged();
                OnPropertyChanged("Destination");
            }
        }

        public Point Destination
        {
            get { return _destination; }
            private set
            {
                if (value.Equals(_destination)) return;
                _destination = value;
                OnPropertyChanged();
            }
        }

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
        
        public double Rotation
        {
            get { return _rotation; }
            set
            {
                if (value.Equals(_rotation)) return;
                _rotation = value;
                OnPropertyChanged();
            }
        }
        
        public int Damage
        {
            get { return _damage; }
            set
            {
                if (value == _damage) return;
                _damage = value;
                OnPropertyChanged();
            }
        }
        
        public virtual double XPosition
        {
            get { return _xPosition; }
            set 
            {
                if (value.Equals(_xPosition)) return;
                _xPosition = value;
                OnPropertyChanged();
                OnPropertyChanged("Destination");
                recalculateRotation();
            }
        }

            protected double getXPosition()
            {
                return _xPosition;
            }

        public virtual double YPosition
        {
            get { return _yPosition; }
            set 
            {
                if (value.Equals(_yPosition)) return;
                _yPosition = value;
                OnPropertyChanged();
                OnPropertyChanged("Destination");
                recalculateRotation();
            }
        }

            protected double getYPosition()
            {
                return _yPosition;
            }

        public bool OnStage
        {
            get
            {
                return _xPosition >= 0 && _xPosition <= Dimensions.StageWidth
                       && _yPosition >= 0 && _yPosition <= Dimensions.StageHeight;
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value.Equals(_enabled)) return;
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public bool Expired
        {
            get { return _expired; }
            set
            {
                if (value.Equals(_expired)) return;
                _expired = value;
                OnPropertyChanged();
                if (!_expired) OnExpired(this, null);
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (value.Equals(_height)) return;
                _height = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                if (value.Equals(_width)) return;
                _width = value;
                OnPropertyChanged();
            }
        }

        public bool Attackable
        {
            get { return _attackable; }
            set
            {
                if (value.Equals(_attackable)) return;
                _attackable = value;
                OnPropertyChanged();
            }
        }

        public Point Center { get { return this.GetCenter(); }}

        public Rect HitBox
        {
            get
            {
                return new Rect(
                    new Point(_xPosition, _yPosition),
                    new Size(_width, _height));
            }
        }

        protected abstract void recalculateRotation();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
