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
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        private Tuple<Dock, double, DateTime> _movedInfo;

        private const double _minMoveRate = .71;
        private const double _maxMoveRate = 10.78;
        private const double _moveRateStep = 1.878;

        private readonly TimeSpan _accelerationPulse = TimeSpan.FromMilliseconds(23);
        private readonly TimeSpan _refreshTime = TimeSpan.FromMilliseconds(18);
        private bool _movingLeft;
        private bool _movingDown;
        private bool _movingRight;
        private bool _movingUp;

        public bool MovingLeft
        {
            get { return _movingLeft; }
            set
            {
                if (value.Equals(_movingLeft)) return;
                _movingLeft = value;
                OnPropertyChanged();
            }
        }

        public bool MovingDown
        {
            get { return _movingDown; }
            set
            {
                if (value.Equals(_movingDown)) return;
                _movingDown = value;
                OnPropertyChanged();
            }
        }

        public bool MovingRight
        {
            get { return _movingRight; }
            set
            {
                if (value.Equals(_movingRight)) return;
                _movingRight = value;
                OnPropertyChanged();
            }
        }

        public bool MovingUp
        {
            get { return _movingUp; }
            set
            {
                if (value.Equals(_movingUp)) return;
                _movingUp = value;
                OnPropertyChanged();
            }
        }

        private void initMovements()
        {
            handleMovementStageChanges();
        }

            private void handleMovementStageChanges()
            {
                StateChanged += (sender, arg) =>
                {
                    if (arg.NewGameState == GameStates.Playing)
                        subscribeMovements();
                    else
                        unsubscribeMovements(); 
                };
            }

                private void unsubscribeMovements()
                {
                    MovingDown = MovingLeft =
                        MovingRight = MovingUp = false;
                    releaseAcceleration();

                    Controls.MoveDownPressed -= onControlWhileMoveDownPressed;
                    Controls.MoveUpPressed -= onControlsOnWhileMoveUpPressed;
                    Controls.MoveLeftPressed -= onControlsWhileMoveLeftPressed;
                    Controls.MoveRightPressed -= onControlsWhileMoveRightPressed;

                    Controls.MoveDownReleased -= (o, eventArgs) => { };
                    Controls.MoveUpReleased -= (o, eventArgs) => { };
                    Controls.MoveLeftReleased -= (o, e) => { };
                    Controls.MoveRightReleased -= (o, e) => { };

                }

                private void subscribeMovements()
                {
                    Controls.MoveDownPressed += onControlWhileMoveDownPressed;
                    Controls.MoveUpPressed += onControlsOnWhileMoveUpPressed;
                    Controls.MoveLeftPressed += onControlsWhileMoveLeftPressed;
                    Controls.MoveRightPressed += onControlsWhileMoveRightPressed;

                    Controls.MoveDownReleased += (o, eventArgs) =>
                    {
                        MovingDown = false;
                        releaseAcceleration();
                    };
                    Controls.MoveUpReleased += (o, eventArgs) =>
                    {
                        MovingUp = false;
                        releaseAcceleration();
                    };
                    Controls.MoveLeftReleased += (o, e) =>
                    {
                        MovingLeft = false;
                        releaseAcceleration();

                    };
                    Controls.MoveRightReleased += (o, e) =>
                    {
                        MovingRight = false;
                        releaseAcceleration();
                    };

                }

                    private void releaseAcceleration()
                    {
                        if (!ReferenceEquals(null, _movedInfo))
                            _movedInfo = Tuple.Create(_movedInfo.Item1, 0.0, _movedInfo.Item3);
                    }

                    private void onControlsWhileMoveRightPressed(object o, KeyEventArgs e)
                    {
                        if (!MovingRight)
                        {
                            MovingRight = true;
                            ThreadPool.QueueUserWorkItem(s =>
                            {
                                var t = DateTime.Now;
                                do
                                {
                                    if (DateTime.Now.Subtract(t) >= _refreshTime
                                        && Ship.HitBox.Right < Dimensions.StageWidth)
                                    {
                                        accelerateTo(Dock.Right);
                                        Ship.XPosition += _movedInfo.Item2;
                                        t = DateTime.Now;
                                    }
                                } while (MovingRight);
                            });
                        }
                    }

                    private void onControlsWhileMoveLeftPressed(object o, KeyEventArgs e)
                    {
                        if (!MovingLeft)
                        {
                            MovingLeft = true;
                            ThreadPool.QueueUserWorkItem(s =>
                            {
                                var t = DateTime.Now;
                                do
                                {
                                    if (DateTime.Now.Subtract(t) >= _refreshTime
                                        && Ship.XPosition > 0)
                                    {
                                        accelerateTo(Dock.Left);
                                        Ship.XPosition -= _movedInfo.Item2;
                                        t = DateTime.Now;
                                    }
                                } while (MovingLeft);
                            });
                        }
                    }

                    private void onControlsOnWhileMoveUpPressed(object o, KeyEventArgs eventArgs)
                    {
                        if (!MovingUp)
                        {
                            MovingUp = true;
                            ThreadPool.QueueUserWorkItem(s =>
                            {
                                var t = DateTime.Now;
                                do
                                {
                                    if (DateTime.Now.Subtract(t) >= _refreshTime
                                        && Ship.HitBox.Top > 0)
                                    {
                                        accelerateTo(Dock.Top);
                                        Ship.YPosition -= _movedInfo.Item2;
                                        t = DateTime.Now;
                                    }
                                } while (MovingUp);
                            });
                        }
                    }

                    private void onControlWhileMoveDownPressed(object o, KeyEventArgs eventArgs)
                    {
                        if (!MovingDown)
                        {
                            MovingDown = true;
                            ThreadPool.QueueUserWorkItem(s =>
                            {
                                var t = DateTime.Now;
                                do
                                {
                                    if (DateTime.Now.Subtract(t) >= _refreshTime
                                        && Ship.HitBox.Bottom < Dimensions.StageHeight)
                                    {
                                        accelerateTo(Dock.Bottom);
                                        Ship.YPosition += _movedInfo.Item2;
                                        t = DateTime.Now;
                                    }
                                } while (MovingDown);
                            });
                        }
                    }

                    private void accelerateTo(Dock desired)
                    {
                        if (ReferenceEquals(null, _movedInfo))
                            _movedInfo = Tuple.Create(desired, _minMoveRate, DateTime.Now);
                        else if (DateTime.Now.Subtract(_movedInfo.Item3) >= _accelerationPulse)
                        {
                            if ( (_movedInfo.Item1 == Dock.Left && desired == Dock.Right)
                                  || (desired == Dock.Left && _movedInfo.Item1 == Dock.Right)
                                  || (desired == Dock.Top && _movedInfo.Item1 == Dock.Bottom)
                                  || (desired == Dock.Bottom && _movedInfo.Item1 == Dock.Top))
                                _movedInfo = Tuple.Create(desired, _minMoveRate, DateTime.Now);
                            else if (_movedInfo.Item2 < _maxMoveRate)
                                _movedInfo = Tuple.Create(desired, 
                                    _movedInfo.Item2 + _moveRateStep, DateTime.Now);
                        }
                    }
    }
}
