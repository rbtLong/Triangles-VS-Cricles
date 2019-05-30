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
namespace TrianglesVCircles.Core.Animation.Tween.Easing
{
    public class Bounce : IEasing
    {
        public float EaseOut(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75))
                return c * (7.5625f * t * t) + b;
            if (t < (2 / 2.75))
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            if (t < (2.5 / 2.75))
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }

        public float EaseIn(float t, float b, float c, float d)
        {
            return c - EaseOut(d - t, 0, c, d) + b;
        }

        public float EaseInOut(float t, float b, float c, float d)
        {
            if (t < d / 2) return EaseIn(t * 2, 0, c, d) * 0.5f + b;
            return EaseOut(t * 2 - d, 0, c, d) * .5f + c * 0.5f + b;
        }
    }
}
