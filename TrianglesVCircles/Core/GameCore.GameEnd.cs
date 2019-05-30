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
using System.Net.Mime;
using System.Windows.Forms.VisualStyles;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        
        private void initShipHealth()
        {
            Death += GameCore_Death;
            _ship.HealthChanged += _ship_HealthChanged;
            ShipHit += onShipHit;
        }

        void GameCore_Death(object sender, EventArgs e)
        {
            _ship.Health = 100;
        }

            void _ship_HealthChanged(object sender, Ship.ShipHealthArg e)
            {
                if (_ship.Health <= 0)
                {
                    if (Incarnation == 0)
                    {
                        Pause();
                        GameOver(this, EventArgs.Empty);
                    }
                    else
                    {
                        Incarnation--;
                        Death(this, new EventArgs());
                    }
                }
            }

            private void onShipHit(object sender, int i)
            {
                _ship.Health -= i;
            }
    }
}
