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

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        public event EventHandler<GameStateChangeArgs> StateChanged = delegate { };
        public event EventHandler<GameStateChangeArgs> StateChanging = delegate { };

        private GameStates _state;

        public GameStates State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                var old = _state;
                var changing = new GameStateChangeArgs(old, value);
                StateChanging(this, changing);
                _state = changing.NewGameState;
                OnPropertyChanged();
                StateChanged(this, new GameStateChangeArgs(old, value));
            }
        } 

        public void Start()
        {
            if (State == GameStates.NotStarted)
            {
                State = GameStates.Playing;
            }
        }

        public void Pause()
        {
            if (State == GameStates.Playing)
            {
                foreach (var e in Enemies)
                {
                    e.RandomMover.Pause();
                    e.Attack.Pause();
                }
                if(_currentLevel != null)
                    _currentLevel.Pause();
                State = GameStates.Paused;
            }
        }

        public void RestartLevel()
        {
            Pause();
            Enemies.Clear();
            LoadLevel(_currentLevel.LevelNumber);
            State = GameStates.Playing;
        }

        public void Restart()
        {
            Pause();
            Enemies.Clear();
            LoadLevel(0);
            State = GameStates.Playing;
        }

        public void CloseSession()
        {
            Pause();
            State = GameStates.NotStarted;
            Enemies.Clear();
        }

        public void Continue()
        {
            if (State == GameStates.Paused)
            {
                foreach (var e in Enemies)
                {
                    e.RandomMover.Continue();
                    e.Attack.Continue();
                }
                if (_currentLevel != null)
                    _currentLevel.Continue();
                State = GameStates.Playing;
            }
        }

    }

    public class GameStateChangeArgs : EventArgs
    {
        public GameStates OldGameState { get; private set; }
        public GameStates NewGameState { get; set; }

        public GameStateChangeArgs(GameStates old, GameStates @new)
        {
            OldGameState = old;
            NewGameState = @new;
        }
    }
}
