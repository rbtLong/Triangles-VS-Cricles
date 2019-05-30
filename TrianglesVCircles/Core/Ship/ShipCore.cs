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

namespace TrianglesVCircles.Core.Ship
{
    public class ShipCore : INotifyPropertyChanged, IAttackable
    {
        private double _yPosition;
        private double _xPosition;
        private Rect _hitBox;
        private double _height = Dimensions.ShipHeight;
        private double _width = Dimensions.ShipWidth;
        private bool _attackable;
        private int _health = 100;

        public event EventHandler<ShipHealthArg> HealthChanged = delegate { }; 

        public ShipCore(double xPos, double yBottomPos)
        {                                       
            PlaceShip(new Point(yBottomPos, xPos));
        }

        public ShipCore()
        {
            PlaceShipX(Dimensions.StageCenter.X);
            PlaceShipY(Dimensions.StageHeight);

        }

        public void PlaceShip(Point dest)
        {
            XPosition = dest.X - (Dimensions.ShipWidth/2);
            YPosition = dest.Y - (Dimensions.ShipHeight/2);
        }

        public void PlaceShipX(double x)
        {
            XPosition = x - (Dimensions.ShipWidth / 2);
        }

        public void PlaceShipY(double y)
        {
            YPosition = y - (Dimensions.ShipHeight);
        }

        public Point Center
        {
            get
            {
                return new Point(
                    _xPosition + (Width /2),
                    _yPosition + (Height/2));
            }
        }

        public Rect HitBox
        {
            get
            {
                return new Rect(
                    new Point(_xPosition+10, _yPosition+20),
                    new Size(Width-25, _height-25));
            }
        }

        public double YPosition
        {
            get { return _yPosition; }
            set
            {
                if (value.Equals(_yPosition)) return;
                _yPosition = value;
                OnPropertyChanged();
                OnPropertyChanged("HitBox");
            }
        }

        public double XPosition
        {
            get { return _xPosition; }
            set
            {
                if (value.Equals(_xPosition)) return;
                _xPosition = value;
                OnPropertyChanged();
                OnPropertyChanged("HitBox");
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

        public int Health
        {
            get { return _health; }
            set
            {
                if (value == _health) return;
                var old = _health;
                _health = value;
                OnPropertyChanged();
                HealthChanged(this, new ShipHealthArg(
                    old - value, old, value));
            }
        }



        public Point GetTip(Point size)
        {
            return new Point(XPosition + (size.X), YPosition - size.Y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ShipHealthArg
    {
        public int Delta { get; private set; }
        public int Old { get; private set; }
        public int New { get; private set; }

        public ShipHealthArg(int delta, int old, int @new)
        {
            Delta = delta;
            Old = old;
            New = @new;
        }

    }
}
