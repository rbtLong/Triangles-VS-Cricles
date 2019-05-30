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

namespace TrianglesVCircles.Core.Animation.Tween.Easing
{
    public class Elastic : IEasing
    {
        public float EaseIn(float t, float b, float c, float d)
        {
		    if (t==0) 
            {
                return b;
            }
            if ((t /= d) == 1) 
            {
                return b+c;
            }
            float p = d * .3f;
            float s = p / 4;
            return -(float)(c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
        }

        public float EaseOut(float t, float b, float c, float d)
        {
		    if (t==0) 
            {
                return b;
            }
            if ((t /= d) == 1) 
            {
                return b+c;
            }
            float p = d * .3f;
            float s = p / 4;
		    return (float)(c * Math.Pow(2,-10*t) * Math.Sin((t*d-s)*(2*Math.PI)/p ) + c + b);
	    }

        public float EaseInOut(float t, float b, float c, float d)
        {
            if (t == 0)
            {
                return b;
            }
            if ((t /= d / 2) == 2)
            {
                return b + c;
            }
            float p = d * (.3f * 1.5f);
            float a = c;
            float s = p / 4;
            if (t < 1)
            {
                return -.5f * (float)(a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            }
		    return (float)(a * Math.Pow(2,-10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )*.5 + c + b);
	    }
    }
}
