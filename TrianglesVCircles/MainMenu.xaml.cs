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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TrianglesVCircles.GameControls.Sound;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            Loaded += MainMenu_Loaded;
        }

        void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var b = BindingHelper.Create("Volume", SoundControl.Instance);
            volume.SetBinding(RangeBase.ValueProperty, b);
        }

        public static readonly DependencyProperty ExitCommandProperty = DependencyProperty.Register(
            "ExitCommand", typeof (ICommand), typeof (MainMenu), new PropertyMetadata(default(ICommand)));

        public ICommand ExitCommand
        {
            get { return (ICommand) GetValue(ExitCommandProperty); }
            set { SetValue(ExitCommandProperty, value); }
        }

        public static readonly DependencyProperty StartGameCommandProperty = DependencyProperty.Register(
            "StartGameCommand", typeof (ICommand), typeof (MainMenu), new PropertyMetadata(default(ICommand)));

        public ICommand StartGameCommand
        {
            get { return (ICommand) GetValue(StartGameCommandProperty); }
            set { SetValue(StartGameCommandProperty, value); }
        }

        public static readonly DependencyProperty CreditsCommandProperty = DependencyProperty.Register(
            "CreditsCommand", typeof (ICommand), typeof (MainMenu), new PropertyMetadata(default(ICommand)));

        public ICommand CreditsCommand
        {
            get { return (ICommand) GetValue(CreditsCommandProperty); }
            set { SetValue(CreditsCommandProperty, value); }
        }

        public static readonly DependencyProperty OptionsCommandProperty = DependencyProperty.Register(
            "OptionsCommand", typeof (ICommand), typeof (MainMenu), new PropertyMetadata(default(ICommand)));

        public ICommand OptionsCommand
        {
            get { return (ICommand) GetValue(OptionsCommandProperty); }
            set { SetValue(OptionsCommandProperty, value); }
        }

        public void ToggleOptions()
        {
            HideAllMenu();
            Options.Visibility = Visibility.Visible;
        }

        public void ToggleMain()
        {
            HideAllMenu();
            Main.Visibility = Visibility.Visible;
        }

        public void ToggleCredit()
        {
            HideAllMenu();
            Credits.Visibility = Visibility.Visible;
        }

            private void HideAllMenu()
            {
                Main.Visibility = 
                    Options.Visibility = 
                    Credits.Visibility = Visibility.Hidden;
            }

        private void return_main(object sender, RoutedEventArgs e)
        {
            ToggleMain();
        }

        private void opt_clicked(object sender, RoutedEventArgs e)
        {
            ToggleOptions();
        }

        private void credits_clicked(object sender, RoutedEventArgs e)
        {
            ToggleCredit();
        }

        private void CheatBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Cheat = !MainWindow.Instance.Cheat;
            if (MainWindow.Instance.Cheat)
                CheatBtn.Tag = "Cheat On";
            else
                CheatBtn.Tag = "Cheat Off";

        }
    }
}
