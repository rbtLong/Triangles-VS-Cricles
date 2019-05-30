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
using System.Windows;
using System.Windows.Forms.VisualStyles;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Creeps.Type1;
using TrianglesVCircles.Core.Creeps.Type2;
using TrianglesVCircles.Core.Creeps.Type3;
using TrianglesVCircles.Core.Ship;
using TrianglesVCircles.Helpers;
using TrianglesVCircles.Problems.Images;
using TrianglesVCircles.Problems.Keys;
using TrianglesVCircles.Problems.MathRelated;
using TrianglesVCircles.Problems.MusicTheory;
using TrianglesVCircles.Problems.Words;

namespace TrianglesVCircles.Core.Levels
{
    public sealed class Level2 : BaseLevel
    {
        private Random _rand = new Random(GlobalRandom.Next(782,1333));
        private int wave = 0;
        private int subwave = 0;
        private int killCount = 0;
        public Level2(ref ObservableImmutableList<BaseEnemy> enemies,
            ShipCore ship)
            : base(enemies, ship)
        {


        }

        protected override void InitializeEnemies()
        {
            Timed.Exec(() =>
                OnStageMessage("More Maths, Picture Matching, and Music Theory!"),
                100);

            Timed.Exec(beginWave0, 11000);
            Timed.Exec(() => OnStageMessage("3"), 3000);
            Timed.Exec(() => OnStageMessage("2"), 6000);
            Timed.Exec(() => OnStageMessage("1"), 9000);
        }

        private void beginWave0()
        {
            OnStageMessage("Music Theory!");
            addWaveEnemies(6, 0);

        }

        private void addWaveEnemies(int count, int wave)
        {

            for (var i = 0; i < count; i++)
            {
                var move = new RangedMovements(
                      new Point(10, 35),
                      new Point(15, 65));

                var moveArc = new VerticalWandering(
                  _rand.Next(35, 150),
                  _rand.Next(400, 550),
                  _rand.Next(5, 23),
                  _rand.Next(300, 350));

                BaseEnemy enemy = null;

                if (wave == 0)
                    enemy = getWave0(moveArc);
                else if (wave == 1)
                    enemy = getWave1(move);
                else if (wave == 2)
                    enemy = getWave2(moveArc, enemy, move);


                if (!ReferenceEquals(null, enemy))
                {
                    move.Size = new Size(enemy.Width, enemy.Height);
                    moveArc.Size = new Size(enemy.Width, enemy.Height);
                    if (enemy.MoveStrategy is VerticalWandering)
                        enemy.RandomMover.AutoManageSpeed = true;
                    AddEnemy(enemy);
                }
            }
        }

        private BaseEnemy getWave2(IMoveStrategy vMove,
            BaseEnemy enemy, RangedMovements move)
        {
            var picker = GlobalRandom.Next(0, 6);
            var p = AdvancedMath.PickAny();
            if (picker == 0)
                enemy = new Type2Variant1(p.Item1, p.Item2, Ship, move);
            else if (picker == 1)
                enemy = new Type2Variant2(p.Item1, p.Item2, Ship, move);
            else if (picker == 2)
                enemy = new Type2Variant3(p.Item1, p.Item2, Ship, move);
            else if (picker == 3)
                enemy = new Type2Variant4(p.Item1, p.Item2, Ship, move);
            else if (picker == 4)
                enemy = new Type1Variant3(p.Item1, p.Item2, Ship, move);
            else if (picker == 5)
                enemy = new Type1Variant2(p.Item1, p.Item2, Ship, move);
            return enemy;
        }

        private BaseEnemy getWave1(RangedMovements move)
        {
            return new Type3Variant2(ProblemImages.EasyImages.Pick(), Ship, move);
        }

            private BaseEnemy getWave0(VerticalWandering moveArc)
            {
                BaseEnemy enemy;
                var notes = Enum.GetValues(typeof (TrebleNote));
                var p = _rand.Next(0, notes.Length);
                
                enemy = new Type3Variant1((TrebleNote)p, Ship, moveArc);

                return enemy;
            }

        protected override void OnEnemiesRemoved()
        {

        }

        protected override void OnEnemiesAdded()
        {

        }

        protected override void TimerElapsed()
        {
            if (wave == 0)
                wave0Ticks();
            else if (wave == 1)
                wave1Ticks();
            else if (wave == 2)
                wave2Ticks();
        }

            private void wave2Ticks()
            {
                if (Enemies.All(o => !o.IsAlive))
                {
                    if (ElapsedTime.Elapsed.Minutes > 3
                        || killCount > 60)
                        beginWave2();
                    else
                        addWaveEnemies(_rand.Next(3, 6), 2);
                }
            }

            private void wave0Ticks()
            {
                if (Enemies.All(o => !o.IsAlive))
                {
                    if (ElapsedTime.Elapsed.Minutes > 2
                        || killCount > 60)
                        beginWave1();
                    else
                        addWaveEnemies(_rand.Next(6, 10), 0);
                }
            }

            private void wave1Ticks()
            {
                if (Enemies.All(o => !o.IsAlive))
                {
                    if (ElapsedTime.Elapsed.Minutes > 3
                        || killCount > 60)
                        beginWave2();
                    else
                        addWaveEnemies(_rand.Next(3, 6), 1);
                }
            }

        private void beginWave2()
        {
            if (wave == 1)
            {
                OnLifeRestore(3);
                subwave = 0;
                ElapsedTime.Restart();
                wave++;
                OnStageMessage("Advnaced Math");
            }
        }

        private void beginWave1()
        {
            if (wave == 0)
            {
                OnLifeRestore(3);
                subwave = 0;
                ElapsedTime.Restart();
                wave++;
                OnStageMessage("Picture Matching");
            }
        }

        protected override string InitializeTitleName()
        {
            return "Level 2";
        }

        protected override int InitializeCurrentLevel()
        {
            return 2;
        }

        public override void PointDeducted(double currentScore)
        {

        }

        public override void PointGained(double currentScore)
        {

        }
    }
}
