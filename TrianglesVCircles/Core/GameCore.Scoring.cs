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
using System.Linq;
using TrianglesVCircles.Core.Affects;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Penalties;
using TrianglesVCircles.Core.Scoring;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        private readonly ScoreSystem _scoring = new ScoreSystem();

        public readonly double WrongButtonScorePenalty = 300;
        public readonly TimeSpan WrongButtonAttackPenalty 
            = TimeSpan.FromMilliseconds(3000);

        public ScoreSystem Scoring
        {
            get { return _scoring; }
        }


        private void initScoring()
        {
            AffectAdded += scoring_AffectAdded;
            _scoring.PointAdded += _scoring_PointAdded;
            _scoring.PointDeducted += _scoring_PointDeducted;
        }

        void _scoring_PointDeducted(object sender, ScoreEntry e)
        {
            _currentLevel.PointDeducted(_scoring.TotalScore);
        }

        void _scoring_PointAdded(object sender, ScoreEntry e)
        {
            _currentLevel.PointGained(_scoring.TotalScore);
        }

            void scoring_AffectAdded(object sender, BaseAffect e)
            {
                _scoring.Scores.Add(new ScoreEntry(
                    e.ScoreDelta, e.Name, DateTime.Now));
            }

        private void wrongButtonPenalty()
        {
            var fx = _shipAffects.FirstOrDefault(o => o is WrongButtonAffect);
            if (!ReferenceEquals(null, fx))
            {
                fx.ExtendDuration();
                _scoring.Scores.Add(new ScoreEntry(
                    fx.ScoreDelta, fx.Name, DateTime.Now));
            }
            else
            {
                _shipAffects.Add(new WrongButtonAffect(
                    _currentLevel.LevelNumber,
                    _shipAffects.Count(o => o is WrongButtonAffect)));                
            }
        }

        private void enemyDestroyedPoint(BaseEnemy enemy)
        {
            _scoring.Scores.DoAdd
                (o => new ScoreEntry(
                    EnemyWorth.GetWorth(enemy), 
                    "Enemy Destroyed",
                    DateTime.Now));
        }

    }
}
