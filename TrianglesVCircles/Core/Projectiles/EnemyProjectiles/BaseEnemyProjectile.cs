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
using TrianglesVCircles.Core.Animation;
using TrianglesVCircles.Core.Animation.Tween.Easing;
using TrianglesVCircles.Core.Projectiles.Attacks;
using TrianglesVCircles.Core.Projectiles.Curvature;

namespace TrianglesVCircles.Core.Projectiles.EnemyProjectiles
{
    public abstract class BaseEnemyProjectile : BaseProjectile, IMovable
    {
        private readonly IAttackable _source;
        private IEasing _ease = new Linear();

        public event EventHandler Hit = delegate { };
        public abstract bool IsLinear { get; }
        public ICurvature Curve { get; private set; }
        public IAttackable Source { get { return _source; } }

        protected BaseEnemyProjectile(
            IAttackable src,
            IAttackable target,
            ICurvature path)
        {
            _source = src;
            Curve = path;
            Target = target;
            XPosition = src.XPosition;
            YPosition = src.YPosition;
        }

        public override sealed double XPosition
        {
            get
            {
                Update();
                return base.XPosition;
            }
            set { base.XPosition = value; }
        }

        public override sealed double YPosition
        {
            get
            {
                Update();
                return base.YPosition;
            }
            set { base.YPosition = value; }
        }

        public override void Update()
        {
            if (HitBox.IntersectsWith(Target.HitBox)
                && Enabled
                && !Expired)
            {
                Hit(this, null);
                Expired = true;
            }
        }

        public override ProjectileTypes Purpose
        {
            get { return ProjectileTypes.FromEnemy; }
        }

    }
}
