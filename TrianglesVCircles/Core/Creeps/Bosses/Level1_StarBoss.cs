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

namespace TrianglesVCircles.Core.Creeps.Bosses
{
    public class Level1_StarBoss : BaseEnemy
    {
        public Level1_StarBoss(string question, string answer,
            IAttackable target, IMoveStrategy moveStrategy)
            : base(question, answer, target, moveStrategy)
        {
        }

        public Level1_StarBoss(string life,
            IAttackable target, IMoveStrategy moveStrategy)
            : base(life, target, moveStrategy)
        {
        }

        public Level1_StarBoss(IEnumerable<string> life,
            IAttackable target, IMoveStrategy moveStrategy)
            : base(life, target, moveStrategy)
        {
        }

        public Level1_StarBoss(char life, IAttackable target,
            IMoveStrategy moveStrategy)
            : base(life, target, moveStrategy)
        {
        }

        protected override int InitializeDamage()
        {
            return 7;
        }

        protected override BaseAttack InitializeAttack()
        {
            return new LinearDirect(this, Target)
            {
                SingleAttack = false,
                Frequency = TimeSpan.FromMilliseconds(70),
                AttackSpeed = TimeSpan.FromMilliseconds(8000),

            };
        }
    }
}
