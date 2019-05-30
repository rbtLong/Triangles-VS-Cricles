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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.GameControls.Backgrounds;
using TrianglesVCircles.GameControls.Sound;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.ViewModel
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private GameViewVm _gameVm = new GameViewVm();
        private Visibility _menuVisibility;
        private Visibility _gameVisibility = Visibility.Collapsed;
        private Visibility _backgroundVisibility;
        private Visibility _storyVisibility = Visibility.Collapsed;

        public GameViewVm GameVm { get { return _gameVm; } }

        public ICommand StartGameCmd
        {
            get
            {
                return new CommandHandler(o =>
                {
                    MenuVisibility = Visibility.Collapsed;
                    _gameVm.StoryStarted += (sender, args) =>
                    {
                        GameVisibility = Visibility.Collapsed;
                        BackgroundVisibility = Visibility.Collapsed;
                        StoryVisibility = Visibility.Visible;
                    };
                    _gameVm.StoryEnded += (sender, args) =>
                    {
                        BackgroundVisibility = Visibility.Visible;
                        StoryVisibility = Visibility.Collapsed;
                        GameVisibility = Visibility.Visible;
                    };
                    _gameVm.LoadLevel(0);
                });
            }
        }

        public ICommand ExitGameCmd
        {
            get
            {
                return new CommandHandler(o => MainWindow.Instance.Close());
            }
        }

        public Visibility MenuVisibility
        {
            get { return _menuVisibility; }
            private set
            {
                if (value == _menuVisibility) return;
                _menuVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility GameVisibility
        {
            get { return _gameVisibility; }
            private set
            {
                if (value == _gameVisibility) return;
                _gameVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility BackgroundVisibility
        {
            get { return _backgroundVisibility; }
            private set
            {
                if (value == _backgroundVisibility) return;
                _backgroundVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility StoryVisibility
        {
            get { return _storyVisibility; }
            private set
            {
                if (value == _storyVisibility) return;
                _storyVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
