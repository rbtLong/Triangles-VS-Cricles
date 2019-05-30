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
using System.Windows.Data;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Core.Projectiles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.GameControls.Creeps
{
    public class BaseEnemyControl : UserControl
    {
        protected event EventHandler ModelChanged = delegate { }; 

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model", typeof (BaseEnemy), typeof (BaseEnemyControl), 
            new PropertyMetadata(default(BaseEnemy), modelChanged));

        private static void modelChanged(
            DependencyObject dObj, 
            DependencyPropertyChangedEventArgs args)
        {
            var src = dObj as BaseEnemyControl;
            src.SetBinding(XPositionProperty, BindingHelper.Create("XPosition", src.Model));
            src.SetBinding(YPositionProperty, BindingHelper.Create("YPosition", src.Model));
            src.SetBinding(ProjectilesProperty, BindingHelper.Create("Projectiles", src.Model.Attack));
            if (src.DataContext is BaseEnemy)
            {
                var ctx = src.DataContext as BaseEnemy;
                ctx.PropertyChanged += (sender, eventArgs) =>
                {
                    if (eventArgs.PropertyName == "IsAlive")
                        if (!ctx.IsAlive)
                            src.AnimateDestroyed();
                };
            }

            var binding = BindingHelper.Create("Caption", src.Model);
            src.SetBinding(CaptionProperty, binding);

            if (src.Model.CaptionDiffers)
                src.RemainingLettersVisibility = Visibility.Visible;
            else
                src.RemainingLettersVisibility = Visibility.Collapsed;

            binding = BindingHelper.Create("AnswersInput", src.Model);
            binding.Mode = BindingMode.OneWay;
            src.SetBinding(RemaindingLifeProperty, binding);

            src.Model.Height = src.ActualHeight;
            src.Model.Width = src.ActualWidth;
            src.ModelChanged(src, EventArgs.Empty);

        }

        public BaseEnemy Model
        {
            get { return (BaseEnemy) GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty XPositionProperty = DependencyProperty.Register(
            "XPosition", typeof (double), typeof (BaseEnemyControl), new PropertyMetadata(default(double)));

        public double XPosition
        {
            get { return (double) GetValue(XPositionProperty); }
            set { SetValue(XPositionProperty, value); }
        }

        public static readonly DependencyProperty YPositionProperty = DependencyProperty.Register(
            "YPosition", typeof (double), typeof (BaseEnemyControl), new PropertyMetadata(default(double)));

        public double YPosition
        {
            get { return (double) GetValue(YPositionProperty); }
            set { SetValue(YPositionProperty, value); }
        }

        public static readonly DependencyProperty ProjectilesProperty = DependencyProperty.Register(
            "Projectiles", typeof (ObservableImmutableList<IProjectile>), typeof (BaseEnemyControl), 
            new PropertyMetadata(default(ObservableImmutableList<IProjectile>)));

        public ObservableImmutableList<IProjectile> Projectiles
        {
            get { return (ObservableImmutableList<IProjectile>) GetValue(ProjectilesProperty); }
            set { SetValue(ProjectilesProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            "Caption", typeof (string), typeof (BaseEnemyControl), new PropertyMetadata(default(string)));

        public string Caption
        {
            get { return (string) GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }


        public static readonly DependencyProperty RemainingLettersVisibilityProperty = DependencyProperty.Register(
            "RemainingLettersVisibility", typeof (Visibility), typeof (BaseEnemyControl), new PropertyMetadata(default(Visibility)));

        public Visibility RemainingLettersVisibility
        {
            get { return (Visibility) GetValue(RemainingLettersVisibilityProperty); }
            set { SetValue(RemainingLettersVisibilityProperty, value); }
        }

        public static readonly DependencyProperty RemaindingLifeProperty = DependencyProperty.Register(
            "RemaindingLife", typeof (string), typeof (BaseEnemyControl), new PropertyMetadata(default(string)));

        public string RemaindingLife
        {
            get { return (string) GetValue(RemaindingLifeProperty); }
            set { SetValue(RemaindingLifeProperty, value); }
        }

        public BaseEnemyControl()
        {

            Loaded += (sender, args) =>
            {
                RenderTransformOrigin = new Point(.5, .5);
                this.PlayForeverAndRandomize("SubtleAnimation");
                this.AnimateEnemyIn();
            };
        }

    }
}
