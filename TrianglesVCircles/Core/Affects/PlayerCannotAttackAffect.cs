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
using TrianglesVCircles.Core.Penalties;

namespace TrianglesVCircles.Core.Affects
{
    public abstract class PlayerCannotAttackAffect : BaseAffect
    {
        public readonly double BasePointDeduction = 300;
        public readonly double BaseLevel_DeductionMultiplier = 7.8;
        public readonly TimeSpan WrongButton_AttackTimerPenalty = TimeSpan.FromMilliseconds(3000);
        public readonly double BaseLevel_TimespanMultiplier = 2.87;
        public readonly double ConsecutiveScoreMultiplier = 3;
        public readonly double ConsecutiveTimeMultiplier = 1.5;
        public readonly double MaxDurationCap = 15;

        public int Level { get; private set; }

        protected PlayerCannotAttackAffect(int level, int consecutive)
        {
            _consecutive = consecutive;
            init(level);
        }

        protected PlayerCannotAttackAffect(int level)
        {
            Level = level;
            init(level);
        }

            private void init(int level)
            {
                Duration = calculateDuration(level);
                _level = level;
                _scoreDelta = -1*(BasePointDeduction
                                  + (level*BaseLevel_DeductionMultiplier)
                                  + (_consecutive*ConsecutiveScoreMultiplier));
            }

                private TimeSpan calculateDuration(int level)
                {
                    return WrongButton_AttackTimerPenalty
                           + TimeSpan.FromMilliseconds(BaseLevel_TimespanMultiplier*level);
                }

        public override void ExtendDuration()
        {
            if (Duration.TotalSeconds <= MaxDurationCap*(Level + 1))
                Duration += calculateDuration(Level);
        }
    }
}
