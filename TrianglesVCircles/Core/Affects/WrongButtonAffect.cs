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

namespace TrianglesVCircles.Core.Affects
{
    public class WrongButtonAffect : PlayerCannotAttackAffect
    {
        public readonly double BasePointDeduction = 300;
        public readonly double BaseLevel_DeductionMultiplier = 7.8;
        public readonly TimeSpan WrongButton_AttackTimerPenalty = TimeSpan.FromMilliseconds(3000);
        public readonly double BaseLevel_TimespanMultiplier = 2.87;
        public readonly double ConsecutiveScoreMultiplier = 3;
        public readonly double ConsecutiveTimeMultiplier = 1.5;

        public WrongButtonAffect(int level, int consecutive)
            : base(level, consecutive)
        {
        }

        public WrongButtonAffect(int level)
            : base(level)
        {
        }

        public override string Name
        {
            get { return "Wrong Button Affect"; }
        }
    }
}

