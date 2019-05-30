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
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies
{
    public class PolarMovements : IMoveStrategy
    {
        public double GetNextY(double pos, Point max)
        {
            var d = 0;
            if (pos <= max.Y * .15)
            {
                d = GlobalRandom.Next(0,
                    (int)(max.Y * .21));
            }
            else if (pos >= max.Y * .15
                     && pos <= max.Y * .50)
            {
                d = GlobalRandom.Next(
                    0, (int)(max.Y * .11));
            }
            else if (pos >= max.Y * .50
                     && pos <= max.Y * .75)
            {
                d = GlobalRandom.Next(
                    0, (int)(max.Y * .11)) * -1;
            }
            else if (pos >= max.Y * .65
                     && pos <= max.Y * 75)
            {
                d = GlobalRandom.Next(
                    0, (int)(max.Y * .21)) * -1;
            }
            return d;
        }

        public double GetNextX(double pos, Point max)
        {
            var d = 0;
            if (pos <= max.X * .15)
                d = GlobalRandom.Next(0,
                    (int)(max.X * .35));
            else if (pos >= max.X * .15
                     && pos <= max.X * .50)
                d = GlobalRandom.Next(
                    0, (int)(max.X * .15));
            else if (pos >= max.X * .50
                     && pos <= max.X * .75)
                d = GlobalRandom.Next(
                    0, (int)(max.X * .15)) * -1;
            else if (pos >= max.X * .75
                     && pos <= max.X * .95)
                d = GlobalRandom.Next(
                    0, (int)(max.X * .35)) * -1;
            return d;
        }
    }
}
