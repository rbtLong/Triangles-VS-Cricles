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
using TrianglesVCircles.Annotations;

namespace TrianglesVCircles.Core.Projectiles.Curvature
{
    public class ArcCurvature : BaseCurvature
    {
        private double _maxWidth = 400;
        private double _maxHeight = 600;

        public double MaxWidth
        {
            get { return _maxWidth; }
            private set { _maxWidth = value; }
        }

        public double MaxHeight
        {
            get { return _maxHeight; }
            private set { _maxHeight = value; }
        }


        protected double ThetaStart { get; set; }
        protected double ThetaEnd { get; set; }

        public override double GetX(double radius)
        {
            return _maxWidth * Math.Cos((Theta += .0187) % 180);
        }

        public override double GetY(double radius)
        {
            return _maxHeight * Math.Sin((Theta += .0187) % 180);
        }

        public ArcCurvature() : base(0) { }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
