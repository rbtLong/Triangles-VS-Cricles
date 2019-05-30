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
using TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Creeps.Bosses;
using TrianglesVCircles.Core.Creeps.Type1;
using TrianglesVCircles.Core.Creeps.Type2;
using TrianglesVCircles.Core.Ship;
using TrianglesVCircles.Helpers;
using TrianglesVCircles.Problems.Keys;
using TrianglesVCircles.Problems.MathRelated;
using TrianglesVCircles.Problems.Words;

namespace TrianglesVCircles.Core.Levels
{
    public sealed class Level1 : BaseLevel
    {
        private Random _random = new Random(GlobalRandom.Next(241, 657));
        private int wave = -1;
        private int subwave = -1;
        private int killCount;

        private readonly string[] _wave0Msgs =
        {
            "Keep fighting!",
            "That's one wave done!!",
            "Here comes more!!",
            "Hint: If you destroy an enemy,\nthe projectiles go too.",
            "GO GO GO GO!!!"
        };



        public Level1(ref ObservableImmutableList<BaseEnemy> enemies,
            ShipCore ship)
            : base(enemies, ship)
        {

        }

            private void addWaveEnemies(int count, int wave, bool useOldMonsters = false)
            {

                for (var i = 0; i < count; i++)
                {
                    var move = new RangedMovements(
                          new Point(10, 35),
                          new Point(15, 65));

                    var moveArc = new VerticalWandering(
                      _random.Next(35, 150),
                      _random.Next(400, 550),
                      _random.Next(5, 23),
                      _random.Next(300, 350));

                    BaseEnemy enemy = null;

                    if(wave == 0)
                        enemy = getWave0(useOldMonsters, moveArc, enemy, move);
                    else if(wave == 1)
                        enemy = getWave1(useOldMonsters, moveArc, enemy, move);
                    else if(wave ==2)
                        enemy = getWave2(useOldMonsters, moveArc, enemy, move);

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

            private BaseEnemy getWave0(bool useOldMonsters, IMoveStrategy vMove, 
                BaseEnemy enemy, RangedMovements move)
            {
                int pos = _random.Next(ProblemKeys.EntireKeyboard.Length);

                var ch = '\0';

                if (_random.Next(0, 10) % 2 == 0)
                    ch = ProblemKeys.EntireKeyboard[pos];
                else
                    ch = ProblemKeys.EntireKeyboard[pos];

                var picker = _random.Next(0, 2);
                if (useOldMonsters)
                    picker = _random.Next(0, 6);
                if (picker == 0)
                    enemy = new Type2Variant1(ch, Ship, move);
                else if (picker == 1)
                    enemy = new Type2Variant1(ch, Ship, vMove);
                else if (picker == 2)
                    enemy = new Type1Variant1(ch, Ship, move);
                else if (picker == 3)
                    enemy = new Type1Variant2(ch, Ship, vMove);
                else if (picker == 4)
                    enemy = new Type1Variant3(ch, Ship, move);
                else if (picker == 5)
                    enemy = new Type1Variant1_1(ch, Ship, move);
                return enemy;
            }

            private BaseEnemy getWave1(bool useOldMonsters, IMoveStrategy vMove, 
                BaseEnemy enemy, RangedMovements move)
            {

                var word = ProblemWords.ShortWords.Pick();
                var picker = _random.Next(0, 2);
                if (useOldMonsters)
                    picker = _random.Next(0, 6);
                if (picker == 0)
                    enemy = new Type2Variant1(word, Ship, move);
                else if (picker == 1)
                    enemy = new Type2Variant2(word, Ship, move);
                else if (picker == 2)
                    enemy = new Type1Variant1(word, Ship, move);
                else if (picker == 3)
                    enemy = new Type1Variant2(word, Ship, move);
                else if (picker == 4)
                    enemy = new Type1Variant3(word, Ship, vMove);
                else if (picker == 5)
                    enemy = new Type1Variant1_1(word, Ship, move);
                return enemy;
            }

            private BaseEnemy getWave2(bool useOldMonsters, IMoveStrategy vMove,
                BaseEnemy enemy, RangedMovements move)
            {
                int pos = _random.Next(ProblemKeys.EntireKeyboard.Length);

                var ch = '\0';

                if (_random.Next(0, 10) % 2 == 0)
                    ch = ProblemKeys.EntireKeyboard[pos];
                else
                    ch = ProblemKeys.EntireKeyboard[pos];
                
                var math = new MathProblem(1,12);
                var p = Tuple.Create("1+1", "2");
                if (subwave == 0) p = math.AddProblem();
                if (subwave == 1) p = math.SubtractProblem();
                if (subwave == 2) p = math.MultiplicationProblem();
                if (subwave == 3) p = math.DivisionProblem();
                if (subwave == 4) p = math.GetRandomProblem();
                p = Tuple.Create(p.Item1.Replace(" ", ""), p.Item2);

                var picker = _random.Next(0, 2);
                if (useOldMonsters)
                    picker = _random.Next(0, 19);

                if (picker == 0)
                    enemy = new Type2Variant1(p.Item1, p.Item2, Ship, move);
                else if (picker  <= 8)
                    enemy = new Type2Variant2(p.Item1,p.Item2, Ship, move);
                else if (picker > 8 && picker <= 16)
                    enemy = new Type1Variant1(ch, Ship, move);
                else if (picker ==17)
                    enemy = new Type1Variant2(ch, Ship, move);
                else if (picker == 18)
                    enemy = new Type1Variant3(ch, Ship, vMove);
                else if (picker == 19)
                    enemy = new Type1Variant1_1(p.Item1, p.Item2, Ship, move);
                return enemy;
            }


        protected override void InitializeEnemies()
        {

            Timed.Exec(() => OnStageMessage("Words, Math, and Letters"), 100);
            Timed.Exec(() => OnStageMessage("3"), 3000);
            Timed.Exec(() => OnStageMessage("2"), 6000);
            Timed.Exec(() => OnStageMessage("1"), 9000);
            Timed.Exec(beginWave0, 11000);

            //Timed.Exec(OnLevelComplete, 100);

            //uncomment this for boss fight (comment the prior)
            //wave = 2;
            //beginBossFight();

        }

        private void beginWave0()
        {
            addWaveEnemies(20,0);
            OnStageMessage("Wave 0\nMore Letters!!");
            wave = 0;
        }


        protected override void OnEnemiesRemoved()
        {
            killCount++;
            if (wave == 0)
            {
                OnHealthRestore(_random.Next(2, 5));
            }

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
            else if (wave == 3)
                bossFightTicks();
        }

        private void wave0Ticks()
            {
                if ((killCount >= 90
                    || ElapsedTime.Elapsed >= TimeSpan.FromMinutes(3))
                    && Enemies.All(o => !o.IsAlive))
                {
                    OnStageMessage("Wave 0 Complete!\n Great Job");
                    Timed.Exec(beginWave1,2000);
                }
                else if ((ElapsedTime.Elapsed < TimeSpan.FromMinutes(3))
                    && Enemies.All(o => !o.IsAlive))
                {
                    addWaveEnemies(_random.Next(23, 30), 0, true);
                    OnStageMessage(_wave0Msgs.Pick());
                    OnHealthRestore(35);
                }

            }

                private void beginWave1()
                {
                    if (wave == 0)
                    {
                        killCount = 0;
                        ElapsedTime.Restart();
                        wave++;
                        OnStageMessage("Wave 1\nWords!");
                        addWaveEnemies(_random.Next(4, 8), 1, true);
                    }
                }

                    private void wave1Ticks()
                    {
                        if ((killCount >= 90
                            || ElapsedTime.Elapsed >= TimeSpan.FromMinutes(3))
                            && Enemies.All(o => !o.IsAlive))
                        {
                            OnStageMessage("Wave 1 Complete!\n Great Job");
                            Timed.Exec(beginWave2, 2000);
                        }
                        else if ((ElapsedTime.Elapsed < TimeSpan.FromMinutes(3))
                            && Enemies.All(o => !o.IsAlive))
                        {
                            addWaveEnemies(_random.Next(12, 16), 1, true);
                            OnStageMessage(_wave0Msgs.Pick());
                        }

                    }

                private void beginWave2()
                {
                    if(wave == 1)
                    {
                        OnLifeRestore(3);
                        ElapsedTime.Restart();
                        killCount = 0;
                        subwave = -1;
                        wave=2;
                        OnStageMessage("Wave 2\nMath!");
                    }

                }

                    private void wave2Ticks()
                    {
                        if ((killCount >= 90
                            || ElapsedTime.Elapsed >= TimeSpan.FromMinutes(3))
                            && Enemies.All(o => !o.IsAlive))
                        {
                            OnStageMessage("Wave 2 Complete!\n Great Job");
                            Timed.Exec(beginBossFight, 2000);
                        }
                        else if ((ElapsedTime.Elapsed < TimeSpan.FromMinutes(3))
                            && Enemies.All(o => !o.IsAlive))
                        {
                            subwave++;
                            if (subwave == 0) OnStageMessage("Math: Adding...");
                            else if (subwave == 1) OnStageMessage("Math: Subtracting...");
                            else if (subwave == 2) OnStageMessage("Math: Multiplying...");
                            else if (subwave == 3) OnStageMessage("Math: Dividing...");
                            else if (subwave == 4) OnStageMessage("Math: All Operations!");
                            addWaveEnemies(_random.Next(10, 16), 2, true);
                        }

                    }

                private void beginBossFight()
                {
                    if (wave == 2)
                    {
                        subwave = 0;
                        addBossWord();
                        OnStageMessage("Level 1 Boss!");
                        OnLifeRestore(1);
                        wave = 3;
                    }

                }

                    private void addBossWord()
                    {
                        var move = new RangedMovements(
                            new Point(10, 55),
                            new Point(15, 75));
                        var b = new Level1_StarBoss(
                            ProblemWords.ShortWords.Pick(), Ship, move);
                        move.Size = new Size(b.Width, b.Height);
                        AddEnemy(b);
                    }

                    private void addBossMath()
                    {
                        var move = new RangedMovements(
                            new Point(10, 55),
                            new Point(15, 75));
                        var p1 = new MathProblem(1, 9).GetRandomProblem();
                        var b = new Level1_StarBoss(p1.Item1, p1.Item2, Ship, move);
                        move.Size = new Size(b.Width, b.Height);
                        AddEnemy(b);
                    }

                private void bossFightTicks()
                {
                    if (Enemies.All(o => !o.IsAlive))
                    {
                        if (subwave == 0)
                        {
                            addBossWord();
                            addBossMath();
                            subwave++;
                        }
                        else if (subwave == 1)
                        {
                            addBossWord();
                            addBossMath(); 
                            addBossWord();
                            addBossMath();
                            subwave++;
                            OnHealthRestore(100);
                        }
                        else if (subwave == 2)
                        {
                            addBossWord();
                            addBossMath();
                            addWaveEnemies(6, 1, true);
                            addWaveEnemies(3, 2);
                            subwave++;
                        }
                        else if (subwave == 3)
                        {
                            OnStageMessage("Level 1 Complete!");
                            Timed.Exec(OnLevelComplete, 2000);
                            subwave++;
                        }
                    }
                }
        protected override string InitializeTitleName()
        {
            return "Level 1";
        }

        protected override int InitializeCurrentLevel()
        {
            return 1;
        }

        public override void PointDeducted(double currentScore)
        {
        }

        public override void PointGained(double currentScore)
        {
        }
    }
}
