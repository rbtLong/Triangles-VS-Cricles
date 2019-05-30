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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core;
using TrianglesVCircles.Core.Levels;
using TrianglesVCircles.GameControls.Backgrounds;
using TrianglesVCircles.GameControls.Sound;
using TrianglesVCircles.GameControls.Story;

namespace TrianglesVCircles.ViewModel
{
    public class GameViewVm : INotifyPropertyChanged, IDisposable
    {
        private GameCore _game;

        public GameCore Game { get { return _game; } }

        public event EventHandler StoryStarted = delegate { };
        public event EventHandler StoryEnded = delegate { };
        public event EventHandler ReturnToMenu = delegate { }; 

        public GameViewVm()
        {
            _game = new GameCore();
            _game.NewLevel += _game_NewLevel;
        }

        void _game_NewLevel(object sender, BaseLevel e)
        {
            if(e.LevelNumber > 0)
                initLevel(e.LevelNumber);
        }

            private void initLevel(int e)
            {
                var hasStory = StoryControl.Instance.LoadLevel(e);
                if (hasStory)
                {
                    StoryStarted(this, EventArgs.Empty);
                    StoryControl.Instance.Completed += (o, story) =>
                    {
                        StoryEnded(this, null);
                        loadBackgroundAndMusic(e);
                        _game.LoadLevel(e);
                        if(_game.State == GameStates.NotStarted)
                            _game.Start();
                        else if(_game.State == GameStates.Paused)
                            _game.Continue();
                    };
                    StoryControl.Instance.Play();
                }
                else loadBackgroundAndMusic(e);
            }

                private static void loadBackgroundAndMusic(int e)
                {
                    SoundControl.Instance.LoadLevel(e);
                    SoundControl.Instance.Play();
                    BackgroundControl.Instance.LoadLevel(e);
                    BackgroundControl.Instance.Play();
                }

        public void LoadLevel(int e)
        {
            initLevel(e);
        }

        public void Restart()
        {
            _game = new GameCore();
            _game.NewLevel += _game_NewLevel;
            Game.Restart();
        }

        public void RestartLevel()
        {
            Game.RestartLevel();
        }

        public void ReturnToMainMenu()
        {
            ReturnToMenu(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged; 

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _game.Dispose();
            _game = null;
        }
    }
}
