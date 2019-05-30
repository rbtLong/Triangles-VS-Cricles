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
using System.Collections.Generic;
using TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies;
using TrianglesVCircles.Core.Projectiles.Attacks;
using TrianglesVCircles.Problems.Images;

namespace TrianglesVCircles.Core.Creeps.Type3
{
    public class Type3Variant2 : BaseEnemy
    {
        public ProblemImageInfo Data { private set; get; }

        public Type3Variant2(ProblemImageInfo prob,
            IAttackable target, IMoveStrategy moveStrategy)
            : base(prob.Answer, target, moveStrategy)
        {
            _captionDiffers = true;
            Data = prob;
        }


        protected override int InitializeDamage()
        {
            return 12;
        }

        protected override BaseAttack InitializeAttack()
        {
            return new LinearScatter(this, Target, 7, 16)
            {
                Frequency = TimeSpan.FromMilliseconds(600),
                AttackSpeed = TimeSpan.FromMilliseconds(40000),
            };
            
        }
    }
}
