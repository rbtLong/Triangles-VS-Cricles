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
using System.Windows;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies
{
    public class Wandering : IMoveStrategy
    {
        private Random _rand = new Random(GlobalRandom.Next(42,293));
        public Size Size { get; set; }


        public double GetNextY(double pos, Point max)
        {
            var d = pos;
            var minBound = max.X + Size.Height;
            var maxBound = max.Y - Size.Height;

            if (pos > max.Y)
            {
                d = pos - _rand.Next((int)minBound, (int)maxBound);
                d -= Size.Height;
            }
            else if (pos < max.X)
            {
                d = pos + _rand.Next((int)minBound, (int)maxBound);
                d += Size.Height;
            }
            else if (Math.Abs(pos - max.Y) <= Size.Height/5)
            {
                d = pos - _rand.Next((int)minBound, (int)maxBound);
                d -= Size.Height * _rand.Next(1, 3);
            }
            else if (Math.Abs(pos - max.X) <= Size.Height/5)
            {
                d = pos + _rand.Next((int)minBound, (int)maxBound);
                d += Size.Height;
            }
            else
            {

                var moveLeft = _rand.Next(1, 10) % 2 == 0;
                if (moveLeft)
                    d = pos - _rand.Next((int)minBound, (int)maxBound);
                else
                    d = pos + _rand.Next((int)minBound, (int)maxBound);

            }
            return d;
        }

        public double GetNextX(double pos, Point max)
        {
            var d = pos;
            var minBound = max.X + Size.Width;
            var maxBound = max.Y - Size.Width;

            if (pos > max.Y)
            {
                d = pos - _rand.Next((int)minBound, (int)maxBound);
                d -= Size.Width * _rand.Next(4, 8);
            }
            else if (pos < max.X)
            {
                d = pos + _rand.Next((int)minBound, (int)maxBound);
                d += Size.Width * _rand.Next(4, 8);
            }
            else if (Math.Abs(pos - max.Y) <= Size.Width/3)
            {
                d = pos - _rand.Next((int)minBound, (int)maxBound);
                d -= Size.Width * _rand.Next(1, 3);
            }
            else if (Math.Abs(pos - max.X) <= Size.Width/3)
            {
                d = pos + _rand.Next((int)minBound, (int)maxBound);
                d += Size.Width * _rand.Next(1, 3);
            }
            else
            {
                var moveLeft = _rand.Next(1, 10) % 2 == 0;
                if (moveLeft)
                {
                    d = pos - _rand.Next((int)minBound, (int)maxBound);
                    d += Size.Width;
                }
                else
                {
                    d = pos + _rand.Next((int)minBound, (int)maxBound);
                    d -= Size.Width;
                }
            }

            return d;
        }
    }
}
