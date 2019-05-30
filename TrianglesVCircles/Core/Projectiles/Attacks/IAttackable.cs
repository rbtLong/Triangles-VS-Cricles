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

namespace TrianglesVCircles.Core.Projectiles.Attacks
{
    public interface IAttackable
    {
        double XPosition { get; set; }
        double YPosition { get; set; }

        double Height { get; set; }
        double Width { get; set; }

        bool Attackable { get; set; }

        Point Center { get; }

        Rect HitBox { get; }
    }

    public static class AttackableExtensionMethods
    {
        public static Rect CalculateHitBox(this IAttackable src)
        {
            return new Rect(
                new Point(src.XPosition, src.YPosition),
                new Point(src.Width,src.Height));
        }

        public static bool Inside(this IAttackable src, IAttackable attackable)
        {
            return src.HitBox.IntersectsWith(attackable.HitBox);
        }

        public static bool IsHit(this IAttackable src, IAttackable attackable)
        {
            return attackable.Attackable && src.Inside(attackable);
        }

        public static Point GetCenter(this IAttackable src)
        {
            return new Point( 
                src.XPosition + (src.Width/2), 
                src.YPosition + (src.Height/2));
        }

        public static double PathDistance(this Point src, Point target)
        {
            var x = Math.Abs(src.X - target.X);
            var y = Math.Abs(src.Y - target.Y);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static double Distance(this Point src)
        {
            return Math.Sqrt((src.X*src.X) + (src.Y*src.Y));
        }

        public static double XPathDistance(this Point src, Point target)
        {
            var x = Math.Abs(src.X - target.X);
            return Math.Sqrt(x * x);
        }

        public static double YPathDistance(this Point src, Point target)
        {
            var y = Math.Abs(src.Y - target.Y);
            return Math.Sqrt(y * y);
        }

    }
}
