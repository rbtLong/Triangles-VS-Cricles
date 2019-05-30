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

using System.Windows;

namespace TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies
{
    public class VerticalWandering : IMoveStrategy
    {
        private readonly RangedMovements _rangedMoves;
        private readonly VerticalOscillation _vertOsc;

        public double VerticalMax { get; private set; }
        public double VerticalMin { get; private set; }
        public double XRandomMin { get; private set; }
        public double XRandomMax { get; private set; }
        public Size Size { get; set; }

        public bool Forward { get; private set; }

        public VerticalWandering(
            double verticalMin, double verticalMax,
            int xRandomMin, int xRandomMax)
        {
            VerticalMin = verticalMin;
            VerticalMax = verticalMax;
            XRandomMin = xRandomMin;
            XRandomMax = xRandomMax;
            
            var xBound = new Point(xRandomMin, xRandomMax);
            _rangedMoves = new RangedMovements(xBound, xBound);
            _vertOsc = new VerticalOscillation(verticalMin, verticalMax);
        }

        public double GetNextX(double pos, Point bound)
        {
            _rangedMoves.Size = Size;
            return _rangedMoves.GetNextX(pos, bound);
        }

        public double GetNextY(double pos, Point bound)
        {
            return _vertOsc.GetNextY(pos, bound);
        }
    }
}
