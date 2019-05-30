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
using TrianglesVCircles.Core.Creeps.Bosses;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.GameControls.Creeps.Bosses
{
    /// <summary>
    /// Interaction logic for Level1_CoinBossesView.xaml
    /// </summary>
    public partial class Level0_CoinBossView : BaseEnemyControl
    {
        public Level0_CoinBossView()
        {
            InitializeComponent();
            Loaded += Level1_CoinBossView_Loaded;
        }

        void Level1_CoinBossView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model is Level0_CoinBoss)
            {
                var model = (Model as Level0_CoinBoss);
                var binding = BindingHelper.Create("Caption", model);

                model.BossFlipped += Level0_CoinBossView_BossFlipped;
                Caption.SetBinding(TextBlock.TextProperty, binding);
            }

        }

            void Level0_CoinBossView_BossFlipped(object sender, EventArgs e)
            {
                Dispatcher.Invoke(() =>
                {
                    if (Model.Invulnerable)
                        (Resources["FlipAnimation"] as Storyboard).Begin();
                    else
                        (Resources["ReverseFlip"] as Storyboard).Begin();
                });
            }

    }
}
