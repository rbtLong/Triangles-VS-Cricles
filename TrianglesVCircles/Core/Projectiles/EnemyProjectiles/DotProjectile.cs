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

using TrianglesVCircles.Core.Projectiles.Attacks;
using TrianglesVCircles.Core.Projectiles.Curvature;

namespace TrianglesVCircles.Core.Projectiles.EnemyProjectiles
{
    public class DotProjectile : BaseEnemyProjectile
    {
        public bool _isLinear = true;

        public DotProjectile(
            IAttackable src, 
            IAttackable target,
            ICurvature curve,
            int damage = 1)
            : base(src, target, curve)
        {
            _isLinear = false;
            Damage = damage;
        }

        public DotProjectile(
            IAttackable src,
            IAttackable target,
            int damage = 0)
            : base(src, target, null)
        {
            _isLinear = true;
            Damage = damage;
        }

        protected override void recalculateRotation()
        {
            
        }

        public override bool IsLinear { get { return _isLinear; } }
    }
}
