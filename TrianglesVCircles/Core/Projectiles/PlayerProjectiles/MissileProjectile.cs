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
using TrianglesVCircles.Core.Projectiles.Attacks;

namespace TrianglesVCircles.Core.Projectiles.PlayerProjectiles
{
    public class MissileProjectile : BaseProjectile
    {
        public MissileProjectile() { }

        public event EventHandler Hit = delegate { }; 

        public MissileProjectile(IAttackable target, Point initPos)
        {
            Target = target;
            XPosition = initPos.X;
            YPosition = initPos.Y;
            updateDestinationAndAngle();
        }

        public override void Update()
        {
            if (Expired) return;
            updateDestinationAndAngle();
            Hit(this, EventArgs.Empty);
            OnPropertyChanged("IsHit");
            Expired = true;
        }

            private void updateDestinationAndAngle()
            {
                //if (!ReferenceEquals(null, Target)
                //    && !IsHit)
                //{
                //    Destination.XPosition = Target.Center.X;
                //    Destination.YPosition = Target.Center.Y;
                //}
            }

        public bool IsHit
        {
            get { return this.Inside(Target); }
        }

        public override ProjectileTypes Purpose
        {
            get { return ProjectileTypes.FromPlayer; }
        }

        protected override void recalculateRotation()
        {
            Rotation = Math.Atan(Target.YPosition / Target.XPosition);
        }


    }
}
