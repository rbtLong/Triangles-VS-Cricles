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
using System.Windows.Media.Animation;

namespace TrianglesVCircles.GameControls.Story
{
    /// <summary>
    /// Interaction logic for StoryControl.xaml
    /// </summary>
    public partial class StoryControl : UserControl
    {
        private EnumPlayStates _state = EnumPlayStates.NeverStarted;

        public static StoryControl Instance { get; private set; }

        public event EventHandler<IStory> Playing = delegate { };
        public event EventHandler<IStory> Completed = delegate { };
        public event EventHandler<IStory> Paused = delegate { }; 

        public EnumPlayStates State
        {
            get { return _state; }
            set { _state = value; }
        }

        public StoryControl()
        {
            InitializeComponent();
            Instance = this;
        }

            public void Play()
            {
                if (Presenter.Content is IStory
                    && (_state != EnumPlayStates.Playing))
                {
                    var sb = Presenter.Content as IStory;
                    sb.Completed += (sender, args) => Completed(this, sb);
                    _state = EnumPlayStates.Playing;
                    (Presenter.Content as IStory).Play();
                    Playing(this, sb);
                }
            }

            public void Pause()
            {
                if (Presenter.Content is IStory
                    && (_state == EnumPlayStates.Paused))
                {
                    _state = EnumPlayStates.Paused;
                    (Presenter.Content as IStory).Pause();
                    Paused(this, Presenter.Content as IStory);
                }
            }

        public bool LoadLevel(int e)
        {
            switch (e)
            {
                case 0:
                    Presenter.Content = new Introduction();
                    return true;

                default:
                    Presenter.Content = null;
                    break;
            }
            return false;
        }

        private void skip_clicked(object sender, RoutedEventArgs e)
        {
            Pause();
            IStory sb = null;
            if (Presenter.Content is Storyboard)
                sb = Presenter.Content as IStory;
            Presenter.Content = null;
            Completed(this, sb);
        }
    }
}
