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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using TrianglesVCircles.Core.Levels;
using TrianglesVCircles.Helpers;
using TrianglesVCircles.ViewModel;

namespace TrianglesVCircles
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            Loaded += GameView_Loaded;
        }

        void GameView_Loaded(object sender, RoutedEventArgs e)
        {
            MaxWidth = Width = Dimensions.StageWidth;
            MinHeight = Height = Dimensions.StageHeight + Dimensions.BottomStageDetailHeight;

            if (DataContext is GameViewVm)
            {
                var vm = DataContext as GameViewVm;
                vm.Game.NewLevel += GameOnNewLevel;
                vm.Game.LevelMessage += Game_LevelMessage;
                vm.Game.GameOver += GameOnGameOver;
            }
        }

            private void GameOnGameOver(object sender, EventArgs eventArgs)
            {
                Dispatcher.Invoke(() =>
                    Gameover.Visibility = Visibility.Visible, 
                    DispatcherPriority.Send);
            }

        void Game_LevelMessage(object sender, Tuple<BaseLevel, string> e)
        {
            animateMessageDisplay(e.Item2);
        }

        private void GameOnNewLevel(object sender, BaseLevel baseLevel)
        {
            animateLevelDisplay(baseLevel.StageName);
        }

            private void animateLevelDisplay(string stagename)
            {
                Dispatcher.Invoke(() =>
                {
                    LevelDisplay.Text = stagename;
                    (Resources["LevelDisplayAnimation"] as Storyboard).Begin(); 
                });
            }

            private void animateMessageDisplay(string message)
            {
                Dispatcher.Invoke(() =>
                {
                    LevelMessage.Text = message;
                    (Resources["LevelMessageAnimation"] as Storyboard).Begin();
                });
            }

        public ICommand RestartGameCommand
        {
            get
            {
                return new CommandHandler(d =>
                {
                    var vm = DataContext as GameViewVm;
                    vm.Restart();
                });
            }
        }

        public ICommand RestartLevelCommand
        {
            get
            {
                return new CommandHandler(d =>
                {
                    var vm = DataContext as GameViewVm;
                    vm.RestartLevel();
                });
            }
        }

        public ICommand CloseGameCommand
        {
            get
            {
                return new CommandHandler(d => Environment.Exit(0));
            }
        }

        public ICommand MainMenuCommand
        {
            get
            {
                return new CommandHandler(d => 
                    (DataContext as GameViewVm).ReturnToMainMenu());
            }
        }

        public ICommand AddLifeCommand
        {
            get 
            { 
                return new CommandHandler(d =>
                {
                    (DataContext as GameViewVm).Game.Incarnation++;
                }); 
            }
        }


        public ICommand FullHealthCommand
        {
            get
            {
                return new CommandHandler(d =>
                {
                    (DataContext as GameViewVm).Game.Ship.Health = 100;
                });
            }
        }

        public ICommand SkipLevelCommand
        {
            get
            {
                return new CommandHandler(d =>
                {
                    (DataContext as GameViewVm).Game.SkipLevel();
                });
            }
        }

        private void GameView_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.Instance.Cheat)
            {
                (DataContext as GameViewVm).Game.Pause();
                Cheat.Visibility = Visibility.Visible;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            (DataContext as GameViewVm).Game.Continue();
            Cheat.Visibility = Visibility.Collapsed;
        }
    }
}
