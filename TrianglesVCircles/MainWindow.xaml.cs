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

using System.Windows;
using TrianglesVCircles.GameControls.Sound;

namespace TrianglesVCircles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; set; }

        public static readonly DependencyProperty CheatProperty =
            DependencyProperty.Register(
                "Cheat", typeof (bool), typeof (MainWindow),
                new PropertyMetadata(false));

        public bool Cheat
        {
            get { return (bool) GetValue(CheatProperty); }
            set { SetValue(CheatProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Instance = this;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            KeyDown += Controls.OnKeyPressed_Handler;
            KeyUp += Controls.OnKeyUp_Handler;
            Dispatcher.Invoke(() =>
            {
                SoundControl.Instance.LoadSource(Catalog.Music.MainMenu);
                SoundControl.Instance.Play();
            });

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
