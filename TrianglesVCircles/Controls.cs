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
using System.Windows.Input;
using fKeys = System.Windows.Forms.Keys;

namespace TrianglesVCircles
{
    public static class Controls
    {
        public static event EventHandler<KeyEventArgs> MoveLeftPressed = delegate { };
        public static event EventHandler<KeyEventArgs> MoveRightPressed = delegate { };
        public static event EventHandler<KeyEventArgs> MoveUpPressed = delegate { };
        public static event EventHandler<KeyEventArgs> MoveDownPressed = delegate { };
                                        
        public static event EventHandler<KeyEventArgs> MoveLeftReleased = delegate { };
        public static event EventHandler<KeyEventArgs> MoveRightReleased = delegate { };
        public static event EventHandler<KeyEventArgs> MoveUpReleased = delegate { };
        public static event EventHandler<KeyEventArgs> MoveDownReleased = delegate { };

        public static event EventHandler<KeyEventArgs> OnKeyDown = delegate { };
        public static event EventHandler<KeyEventArgs> OnKeyUp = delegate { };

        public static void OnKeyPressed_Handler(object sender, KeyEventArgs e)
        {
            OnKeyDown(null, e);
            if (e.Key == Key.Left) MoveLeftPressed(null, e);
            if (e.Key == Key.Right) MoveRightPressed(null, e);
            if (e.Key == Key.Up) MoveUpPressed(null, e);
            if (e.Key == Key.Down) MoveDownPressed(null, e);
        }

        public static void OnKeyUp_Handler(object sender, KeyEventArgs e)
        {
            OnKeyUp(null, e);
            if (e.Key == Key.Left) MoveLeftReleased(null, e);
            if (e.Key == Key.Right) MoveRightReleased(null, e);
            if (e.Key == Key.Up) MoveUpReleased(null, e);
            if (e.Key == Key.Down) MoveDownReleased(null, e);
        }
    }          
}              
