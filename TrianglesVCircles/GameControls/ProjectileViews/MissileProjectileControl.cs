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
using System.Windows.Media;
using System.Windows.Media.Animation;
using TrianglesVCircles.Core.Projectiles.PlayerProjectiles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.GameControls.ProjectileViews
{
    public class MissileProjectileControl : Control
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof (MissileProjectile), typeof (MissileProjectileControl),
            new PropertyMetadata(new MissileProjectile(), DataChanged));

        private static void DataChanged(
            DependencyObject dependencyObject, 
            DependencyPropertyChangedEventArgs arg)
        {
            var dat = arg.NewValue as MissileProjectile;
            if (ReferenceEquals(null, dat))
                return;

            var src = (dependencyObject as MissileProjectileControl);
            if (ReferenceEquals(null, src.Data)) return;
            src.SetBinding(DestinationProperty, new Binding("Destination") { Source = dat });
            src.SetBinding(XDestinationProperty, new Binding("X") { Source = dat.Destination });
            src.SetBinding(YDestinationProperty, new Binding("Y") { Source = dat.Destination });
            src.SetBinding(XPositionProperty, new Binding("XPosition") { Source = dat });
            src.SetBinding(YPositionProperty, new Binding("YPosition") { Source = dat });

            var ease = new PowerEase { EasingMode = EasingMode.EaseOut, Power = 2.874 };

            var trans = new TranslateTransform();
            var xAnim = new DoubleAnimation { EasingFunction = ease, Duration = TimeSpan.FromMilliseconds(311) };
            xAnim.Completed += (sender, args) => src.AnimationDone = Tuple.Create(true, src.AnimationDone.Item2);
            BindingOperations.SetBinding(xAnim, DoubleAnimation.FromProperty, BindingHelper.Create("XPosition", dat));
            BindingOperations.SetBinding(xAnim, DoubleAnimation.ToProperty, BindingHelper.Create("X", dat.Destination));

            var yAnim = new DoubleAnimation { EasingFunction = ease, Duration = TimeSpan.FromMilliseconds(311) };
            BindingOperations.SetBinding(yAnim, DoubleAnimation.FromProperty, BindingHelper.Create("YPosition", dat));
            BindingOperations.SetBinding(yAnim, DoubleAnimation.ToProperty, BindingHelper.Create("Y", dat.Destination));
            yAnim.Completed += (sender, args) => src.AnimationDone = Tuple.Create(src.AnimationDone.Item1, true);

            var rotTrans = new RotateTransform { CenterX = .5, CenterY = .5 };
            var angleAnim = new DoubleAnimation { EasingFunction = ease, Duration = TimeSpan.FromMilliseconds(312) };
            BindingOperations.SetBinding(angleAnim, DoubleAnimation.FromProperty, BindingHelper.Create("Rotation", dat));

            var transGroup = new TransformGroup();
            transGroup.Children.Add(trans);
            transGroup.Children.Add(rotTrans);

            src.RenderTransform = transGroup;

            src.Loaded += (sender, args) =>
            {
                trans.BeginAnimation(TranslateTransform.XProperty, xAnim);
                trans.BeginAnimation(TranslateTransform.YProperty, yAnim);
                trans.BeginAnimation(RotateTransform.AngleProperty, angleAnim);
            };
        }


        public MissileProjectile Data
        {
            get { return (MissileProjectile) GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register(
            "Destination", typeof (Point), typeof (MissileProjectileControl), new PropertyMetadata(default(Point)));

        public Point Destination
        {
            get { return (Point) GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }

        public static readonly DependencyProperty XDestinationProperty = DependencyProperty.Register(
            "XDestination", typeof (double), typeof (MissileProjectileControl), new PropertyMetadata(default(double)));

        public double XDestination
        {
            get { return (double) GetValue(XDestinationProperty); }
            set { SetValue(XDestinationProperty, value); }
        }

        public static readonly DependencyProperty YDestinationProperty = DependencyProperty.Register(
            "YDestination", typeof (Double), typeof (MissileProjectileControl), new PropertyMetadata(default(Double)));

        public Double YDestination
        {
            get { return (Double) GetValue(YDestinationProperty); }
            set { SetValue(YDestinationProperty, value); }
        }

        public static readonly DependencyProperty XPositionProperty = DependencyProperty.Register(
            "XPosition", typeof (double), typeof (MissileProjectileControl), new PropertyMetadata(default(double)));

        public double XPosition
        {
            get { return (double) GetValue(XPositionProperty); }
            set { SetValue(XPositionProperty, value); }
        }

        public static readonly DependencyProperty YPositionProperty = DependencyProperty.Register(
            "YPosition", typeof (double), typeof (MissileProjectileControl), new PropertyMetadata(default(double)));

        public double YPosition
        {
            get { return (double) GetValue(YPositionProperty); }
            set { SetValue(YPositionProperty, value); }
        }

        public static readonly DependencyProperty AnimationDoneProperty = DependencyProperty.Register(
            "AnimationDone", typeof (Tuple<bool, bool>), 
            typeof (MissileProjectileControl),
            new PropertyMetadata(new Tuple<bool, bool>(false, false),
                (o, args) =>
                {
                    var src = (o as MissileProjectileControl);
                    src.Data.Update();
                    if (src.AnimationDone.Item1 && src.AnimationDone.Item2)
                    {
                        src.Data.Damage = 1;
                        if (!src.Data.IsHit)
                        {
                            src.Data.XPosition = src.XDestination;
                            src.Data.YPosition = src.YDestination;
                        }
                    }
                }));

        public Tuple<bool, bool> AnimationDone
        {
            get { return (Tuple<bool, bool>) GetValue(AnimationDoneProperty); }
            set { SetValue(AnimationDoneProperty, value); }
        }

    }
}
