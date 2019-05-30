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
using TrianglesVCircles.Core.Projectiles.EnemyProjectiles;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.GameControls.ProjectileViews.EnemyProjectiles
{
    public class DotProjectileControl : Control
    {
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
     "Data", typeof(DotProjectile), typeof(DotProjectileControl),
     new PropertyMetadata(default(DotProjectile), DataChanged));

        private static void DataChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs arg)
        {
            var dat = arg.NewValue as DotProjectile;
            if (ReferenceEquals(null, dat))
                return;

            var src = (dependencyObject as DotProjectileControl);
            if (ReferenceEquals(null, src.Data)) return;
            
            src.SetBinding(DestinationProperty,  new Binding("Destination") { Source = dat });
            src.SetBinding(XDestinationProperty, new Binding("X") { Source = dat.Destination });
            src.SetBinding(YDestinationProperty, new Binding("Y") { Source = dat.Destination });
            src.SetBinding(XPositionProperty, BindingHelper.Create("XPosition", dat));
            src.SetBinding(YPositionProperty, BindingHelper.Create("YPosition", dat));
            src.SetBinding(VisibilityProperty, BindingHelper.Create("Expired", dat, new InvertedBoolToVisibilityConverter()));
        }


        public DotProjectile Data
        {
            get { return (DotProjectile)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register(
            "Destination", typeof(Point), typeof(DotProjectileControl), new PropertyMetadata(default(Point)));

        public Point Destination
        {
            get { return (Point)GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }

        public static readonly DependencyProperty XDestinationProperty = DependencyProperty.Register(
            "XDestination", typeof(double), typeof(DotProjectileControl), new PropertyMetadata(default(double)));

        public double XDestination
        {
            get { return (double)GetValue(XDestinationProperty); }
            set { SetValue(XDestinationProperty, value); }
        }

        public static readonly DependencyProperty YDestinationProperty = DependencyProperty.Register(
            "YDestination", typeof(Double), typeof(DotProjectileControl), new PropertyMetadata(default(Double)));

        public Double YDestination
        {
            get { return (Double)GetValue(YDestinationProperty); }
            set { SetValue(YDestinationProperty, value); }
        }

        public static readonly DependencyProperty XPositionProperty = DependencyProperty.Register(
            "XPosition", typeof(double), typeof(DotProjectileControl), new PropertyMetadata(default(double)));

        public double XPosition
        {
            get { return (double) GetValue(XPositionProperty); }
            set { SetValue(XPositionProperty, value); }
        }

        public static readonly DependencyProperty YPositionProperty = DependencyProperty.Register(
            "YPosition", typeof(double), typeof(DotProjectileControl), new PropertyMetadata(default(double)));

        public double YPosition
        {
            get { return (double)GetValue(YPositionProperty); }
            set { SetValue(YPositionProperty, value); }
        }
    }
}
