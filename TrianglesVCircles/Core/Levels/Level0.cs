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
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using TrianglesVCircles.Core.Animation.RandomMovements.MoveStrategies;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Creeps.Bosses;
using TrianglesVCircles.Core.Creeps.Type1;
using TrianglesVCircles.Core.Ship;
using TrianglesVCircles.Helpers;
using TrianglesVCircles.Problems.Keys;

namespace TrianglesVCircles.Core.Levels
{
    public sealed class Level0 : BaseLevel
    {
        private Random _random = new Random(GlobalRandom.Next(641, 1070));
        private int wave;
        private int _phase = -1;
        private int _subWave;

        public int Phase
        {
            get { return _phase; }
            private set { _phase = value; }
        }


        public Level0(ref ObservableImmutableList<BaseEnemy> enemies,
            ShipCore ship)
            : base(enemies, ship)
        {

        }

        protected override void InitializeEnemies()
        {
            //Timed.Exec(() => OnLevelComplete(), 10);

            Timed.Exec(() =>
            {
                ElapsedTime.Restart();
                Phase = 0;
                phase0AddEnemies(_random.Next(7, 14));
                OnStageMessage("Phase1\nDodge enemy attacks!!");
            }, 11000);

            Timed.Exec(() => OnStageMessage("Get Ready!"), 100);
            Timed.Exec(() => OnStageMessage("Starting in 3 . . .\nDon't Shoot in Phase 0!"), 3000);
            Timed.Exec(() => OnStageMessage("Starting in 2 . . .\nDon't Shoot in Phase 0!"), 6000);
            Timed.Exec(() => OnStageMessage("Starting in 1 . . .\nDon't Shoot in Phase 0!"), 9000);
        }

        protected override void OnEnemiesRemoved() { }

        protected override void OnEnemiesAdded() { }

        public override void PointDeducted(double currentScore)
        {
            if(Phase == 0) phase0Hints();
            if (Phase == 3 && ElapsedTime.Elapsed < TimeSpan.FromSeconds(30))
                bossHints();
        }

            private void bossHints()
            {
                var hint = this.PickFrom(new[]
                {
                    "Ooops! Try not to attack when its blue!!",
                    "This boss is invincible when its blue!!",
                    "Watch out!! Hold your fire when it's blue!"
                });
                OnStageMessage(hint);
            }

            private void phase0Hints()
                {
                    var dontFireHints = new[]
                    {
                        "Hold your fire!\nTry to ride it out!",
                        "Ooops!! Don't shoot yet, hold on!",
                        "Oh no, don't fire yet!!"
                    };
                    if (Phase == 0 
                        && wave == 0
                        && ElapsedTime.Elapsed < TimeSpan.FromSeconds(50))
                        OnStageMessage(dontFireHints.Pick());
                }

        public override void PointGained(double currentScore)
        {
        }

        protected override void TimerElapsed()
        {
            if(Phase == 0)
                phase0Ticks();
            else if (Phase == 1)
                phase1Ticks();
            else if (Phase == 2)
                phase2Ticks();
            else if (Phase == 3)
                phase3Ticks();
        }

        private void phase3Ticks()
        {
            if (wave == 0 && Lapsed(6))
                OnStageMessage("Remember: Don't attack when it's blue!!");

            if (ElapsedTime.Elapsed > TimeSpan.FromSeconds(10))
            {
                if (Enemies.All(o => !o.IsAlive)
                    && wave == 0)
                {
                    wave = 1;
                    OnStageMessage("Oh no, there's more!?\nHang in there!");
                    addBoss();
                    Timed.Exec(addBoss, 3211);
                }
                else if (Enemies.All(o => !o.IsAlive)
                         && wave == 1)
                {
                    wave = 2;
                    OnStageMessage("Level 0 COMPLETE!!\nYou're Good!");
                }
                else if (Enemies.All(o => !o.IsAlive)
                         && wave == 2)
                {
                    ElapsedTime.Restart();
                    OnHealthRestore(100);
                    Timed.Exec(OnLevelComplete, 1200);
                    Timed.Exec(() => OnStageMessage("Now, prepare yourself!"), 100);
                    wave++;
                }
            }
        }

        private void phase0Ticks()
            {
                if (ElapsedTime.Elapsed >= TimeSpan.FromSeconds(0)
                    && ElapsedTime.Elapsed <= TimeSpan.FromSeconds(45))
                    foreach (var enemies in Enemies)
                        enemies.Invulnerable = true;

                if (ElapsedTime.Elapsed >= TimeSpan.FromSeconds(60)
                    && ElapsedTime.Elapsed <= TimeSpan.FromSeconds(61))
                    foreach (var enemies in Enemies)
                        enemies.Invulnerable = false;

                if (Lapsed(6))
                {
                    OnStageMessage("Remember: just dodge, don't attack!");
                }

                if (Lapsed(15))
                {
                    if(Enemies.Count < 10)
                        phase0AddEnemies(_random.Next(4, 6));
                    OnStageMessage("Keep it up!");
                }
                else if (Lapsed(30))
                {
                    if (Enemies.Count < 13)
                        phase0AddEnemies(_random.Next(6, 10));
                    if (_subWave == 0)
                    {
                        OnHealthRestore(20);
                        _subWave++;
                    }
                    OnStageMessage("Here's a few more!");
                }
                else if (Lapsed(45))
                {
                    if (Enemies.Count < 15)
                        phase0AddEnemies(_random.Next(6, 12));
                    if (_subWave == 1)
                    {
                        OnHealthRestore(20);
                        _subWave++;
                    }
                    OnStageMessage("Keep it up, you're almost there!");
                }
                else if (Lapsed(60))
                {
                    OnStageMessage("Fire away!");
                }


                if (Enemies.All(o => !o.IsAlive)
                    && ElapsedTime.Elapsed >= TimeSpan.FromSeconds(60))
                {
                    OnHealthRestore(100);
                    OnStageMessage("Phase 0 Complete!\n Good JOB!!");
                    Timed.Exec(beginPhase1, 2800);
                }

            }

                private void phase0AddEnemies(int count)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var move = new RangedMovements(
                            new Point(10, 20),
                            new Point(15, 65));

                        var moveArc = new VerticalWandering(
                          _random.Next(25, 100),
                          _random.Next(400, 530),
                          _random.Next(5, 23),
                          _random.Next(300, 350));

                        var r = _random.Next(5);
                        int pos = _random.Next(ProblemKeys.LeftSide_Easiest.Length);

                        BaseEnemy enemy = null;

                        if (r == 0)
                            enemy = new BasicCircle(ProblemKeys.LeftSide_Easiest[pos], Ship, move);
                        else if (r == 1)
                            enemy = new Type1Variant1(ProblemKeys.LeftSide_Easiest[pos], Ship, moveArc);

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

            private void beginPhase1()
            {
                if (Phase == 0)
                {
                    ElapsedTime.Restart();
                    Phase = 1;
                    OnStageMessage("Phase 1\nEnemies are trickier to kill!");
                    phase1AddEnemies(_random.Next(16, 22));
                }

            }

          private void phase1Ticks()
          {


              if (Enemies.All(o => !o.IsAlive))
              {
                  if (wave == 0)
                  {
                      OnStageMessage("It's going to get tougher!");
                      phase1AddEnemies(_random.Next(13, 17));
                      wave = 1;
                  }
                  else if (wave == 1)
                  {
                      OnStageMessage("Hint: Wait until enemies fire, " +
                                     "then move as little as possible.");
                      phase1AddEnemies(_random.Next(22, 28));
                      wave = 2;
                  }
                  else if (wave == 2)
                  {
                      OnStageMessage("Here's a Bonus Life! KEEP GOING!!!");
                      OnLifeRestore(1);
                      phase1AddEnemies(_random.Next(27, 33));
                      wave = 3;
                  }
                  else
                  {
                      OnHealthRestore(100);
                      OnStageMessage("Phase 1 Complete!\n No more handle bars!");
                      Timed.Exec(beginPhase2, 2800);
                  }

              }
          }

              private void phase1AddEnemies(int count)
              {

                  for (var i = 0; i < count; i++)
                  {

                      var move = new RangedMovements(
                        new Point(10, 20),
                        new Point(15, 65));

                      var moveArc = new RangedMovements(
                          new Point(10, 20),
                          new Point(25, 120));

                      new VerticalWandering(
                          _random.Next(30, 130),
                          _random.Next(400, 550),
                          _random.Next(5, 23),
                          _random.Next(300, 350));

                      var r = _random.Next(5);
                      int pos = _random.Next(ProblemKeys.LeftSide_Easy.Length);

                      BaseEnemy enemy = null;

                      if (r == 0)
                          enemy = new BasicCircle(
                              ProblemKeys.LeftSide_Easy[pos], Ship, move);
                      else if (r == 1)
                          enemy = new Type1Variant1(
                              ProblemKeys.LeftSide_Easy[pos], Ship, moveArc);

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

          private void beginPhase2()
          {
              if (Phase == 1)
              {
                  ElapsedTime.Restart();
                  OnHealthRestore(100);
                  Phase = 2;
                  wave = 0;
                  OnStageMessage("Phase 2\nEven more variety!");
                  phase2AddEnemies(_random.Next(22, 27));
              }
          }


          private void phase2Ticks()
          {


              if (Enemies.All(o => !o.IsAlive))
              {
                  if (wave == 0)
                  {
                      OnStageMessage("Be Fast, Be Precise!");
                      phase2AddEnemies(_random.Next(18, 23));
                      wave = 1;
                  }
                  else if (wave == 1)
                  {
                      OnStageMessage("You're improving with both hands!!");
                      phase2AddEnemies(_random.Next(25, 28));
                      wave = 2;
                  }
                  else if (wave == 2)
                  {
                      OnStageMessage("Here's a Bonus Life! KEEP GOING!!!");
                      OnLifeRestore(1);
                      phase2AddEnemies(_random.Next(27, 32));
                      wave = 3;
                  }
                  else
                  {
                      OnHealthRestore(100);
                      OnStageMessage("Phase 2 Complete!\nYou're GOOD!");
                      Timed.Exec(beginBossFight, 2800);
                  }
              }


          }

              private void phase2AddEnemies(int count)
              {

                  for (var i = 0; i < count; i++)
                  {
                      var move = new RangedMovements(
                            new Point(10, 20),
                            new Point(15, 65));

                      var moveArc = new VerticalWandering(
                        _random.Next(35, 150),
                        _random.Next(400, 550),
                        _random.Next(5, 23),
                        _random.Next(300, 350));

                      var r = _random.Next(5);
                      int pos = _random.Next(ProblemKeys.LeftSide_Easy.Length);
                      var ch = '\0';

                      if (_random.Next(0, 10) % 2 == 0)
                          ch = ProblemKeys.LeftSide[pos];
                      else
                          ch = ProblemKeys.RightSide[pos];


                      BaseEnemy enemy = null;

                      if (r == 0)
                          enemy = new BasicCircle(
                              ch, Ship, move);
                      else if (r == 1)
                          enemy = new Type1Variant1(
                              ch, Ship, moveArc);

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


                private void beginBossFight()
                {
                    if (Phase == 2)
                    {
                        wave = 0;
                        ElapsedTime.Restart();
                        OnStageMessage("Level 0 Boss!!!!\nDo not attack when its blue.");
                        Phase = 3;
                        addBoss();
                    }
                }

                    private void addBoss()
                    {
                        var move = new RangedMovements(
                            new Point(10, 20),
                            new Point(15, 65));

                        var life = new List<string>();
                        for (int i = 0; i < _random.Next(5, 8); ++i)
                        {
                            var pos = _random.Next(ProblemKeys.LeftSide_Easy.Length);
                            life.Add(ProblemKeys.LeftSide_Easy[pos].ToString(CultureInfo.InvariantCulture));
                        }

                        var boss = new Level0_CoinBoss(life.AsEnumerable(), Ship, move);
                        move.Size = new Size(boss.Width, boss.Height);
                        AddEnemy(boss);
                    }

        protected override string InitializeTitleName()
        {
            return "Level 0";
        }

        protected override int InitializeCurrentLevel()
        {
            return 0;
        }


    }
}
