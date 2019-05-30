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

namespace TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies
{
    public class VerticalOscillation : IMoveStrategy
    {
        public double Max { get; private set; }
        public double Min { get; private set; }
        public bool Forward { get; private set; }

        public VerticalOscillation(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public double GetNextX(double pos, System.Windows.Point bound)
        {
            return pos;
        }

        public double GetNextY(double pos, System.Windows.Point bound)
        {
            var d = pos;

            if (pos < Min) 
            { 
                d = pos + Math.Abs(pos-Min);
                Forward = true;
            }
            else if (pos > Max) 
            {
                d = pos - pos + Math.Abs(pos - Min);
                Forward = false;
            }
            else if (pos <= Max && pos >= Min)
                if (Forward)
                {
                    if (Math.Abs(d - Max) < 1) d = Min;
                    else d = Max;
                }
                else 
                {
                    if (Math.Abs(d - Min) < 1) d = Max;
                    else d = Min; 
                }

            return d;
        }

    }
}
