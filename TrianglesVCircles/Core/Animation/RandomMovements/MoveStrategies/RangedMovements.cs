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
    public class RangedMovements : IMoveStrategy
    {
        private Random _rand = new Random(GlobalRandom.Next(124,242));
        public Point XBound { get; private set; }
        public Point YBound { get; private set; }

        public Size Size { get; set; }

        public RangedMovements(Point xBound, Point yBound)
        {
            XBound = xBound;
            YBound = yBound;
        }

        public double GetNextY(double pos, Point max)
        {
            var d = 0.0;

            if (pos > max.Y)
            {
                d = pos - _rand.Next((int)YBound.X, (int)YBound.Y);
                d -= Size.Height;
            }
            else if (pos < max.X)
            {
                d = pos + _rand.Next((int)YBound.X, (int)YBound.Y);
                d += Size.Height;
            }
            else if (Math.Abs(pos - max.Y) <= Size.Height/3)
            {
                d = pos - _rand.Next((int)XBound.X, (int)XBound.Y);
                d -= Size.Height * _rand.Next(1, 3);
            }
            else if (Math.Abs(pos - max.X) <= Size.Height/3)
            {
                d = pos + _rand.Next((int)XBound.X, (int)XBound.Y);
                d += Size.Height;
            }
            else
            {
                var moveLeft = _rand.Next(1, 10) % 2 == 0;
                if (moveLeft)
                    d = pos - _rand.Next((int)YBound.X, (int)YBound.Y);
                else
                    d = pos + _rand.Next((int)YBound.X, (int)YBound.Y);

            }
            return d;
        }

        public double GetNextX(double pos, Point max)
        {
            var d = 0.0;

            if (pos > max.Y)
            {
                d = pos - _rand.Next((int)XBound.X, (int)XBound.Y);
                d -= Size.Width * _rand.Next(4, 8);
            }
            else if (pos < max.X)
            {
                d = pos + _rand.Next((int)XBound.X, (int)XBound.Y);
                d += Size.Width * _rand.Next(4, 8);
            }
            else if (Math.Abs(pos - max.Y) <= Size.Width/3)
            {
                d = pos - _rand.Next((int)XBound.X, (int)XBound.Y);
                d -= Size.Width * _rand.Next(1, 3);
            }
            else if (Math.Abs(pos - max.X) <= Size.Width/3)
            {
                d = pos + _rand.Next((int)XBound.X, (int)XBound.Y);
                d += Size.Width * _rand.Next(1, 3);
            }
            else
            {
                var moveLeft = _rand.Next(1, 10) % 2 == 0;
                if (moveLeft)
                {
                    d = pos - _rand.Next((int)XBound.X, (int)XBound.Y);
                    d += Size.Width;
                }
                else
                {
                    d = pos + _rand.Next((int)XBound.X, (int)XBound.Y);
                    d -= Size.Width;
                }
            }

            if (d >= max.Y) d = max.Y - (Size.Width * _rand.Next(0, 3));
            if (d <= max.X) d = max.X + (Size.Width * _rand.Next(0, 3));


            return d;
        }
    }
}
