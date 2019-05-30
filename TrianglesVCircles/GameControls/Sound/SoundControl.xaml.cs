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

namespace TrianglesVCircles.GameControls.Sound
{
    /// <summary>
    /// Interaction logic for SoundControl.xaml
    /// </summary>
    public partial class SoundControl : UserControl
    {
        static public SoundControl Instance { get; private set; }

        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
            "Volume", typeof (double), typeof (SoundControl), new PropertyMetadata(1.0));

        public double Volume
        {
            get { return (double) GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public SoundControl()
        {
            InitializeComponent();
            Instance = this;
        }

        public void LoadSource(string localPath)
        {
            Sound.LoadedBehavior = MediaState.Manual;
            Sound.UnloadedBehavior = MediaState.Manual;
            Sound.MediaEnded += Sound_MediaEnded;
            Sound.Source = new Uri(localPath, UriKind.Relative);
        }

        void Sound_MediaEnded(object sender, RoutedEventArgs e)
        {
            Sound.Position = TimeSpan.FromMilliseconds(1);
        }

        public void LoadLevel(int level)
        {
            switch (level)
            {
                case -1:
                    LoadSource(Catalog.Music.MainMenu);
                    break;
                case 0:
                    LoadSource(Catalog.Music.Level0);
                    break;
                case 1:
                    LoadSource(Catalog.Music.Level1);
                    break;
                case 2:
                    LoadSource(Catalog.Music.Level2);
                    break;
                case 3:
                    LoadSource(Catalog.Music.Level3);
                    break;
                default:
                    LoadSource(Catalog.Music.Level0);
                    break;
            }
        }

        public void Play()
        {
            Sound.Play();
        }

        public void Stop()
        {
            Sound.Stop();
        }

        public void Pause()
        {
            Sound.Pause();
        }
    }
}
