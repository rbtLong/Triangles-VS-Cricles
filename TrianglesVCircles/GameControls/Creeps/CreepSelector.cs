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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TrianglesVCircles.Core.Creeps;
using TrianglesVCircles.Helpers;

/*
 * Not used anymore, but made some really useful
 * and interesting stuff.
 */

namespace TrianglesVCircles.GameControls.Creeps
{
    public class CreepSelector : DataTemplateSelector
    {


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!ReferenceEquals(container,null)
                && container is FrameworkElement
                && (container as FrameworkElement).DataContext is BaseEnemy)
            {
                var ctx = (container as FrameworkElement).DataContext as BaseEnemy;
                var tModelName = ctx.GetType().GetTypeInfo().Name;
                var tView = Assembly.GetAssembly(item.GetType())
                    .GetTypes().FirstOrDefault(o => o.Name == tModelName + "View");

                if (!ReferenceEquals(tView, null))
                {
                    dynamic view = Activator.CreateInstance(tView);
                    view.DataContext = view;

                    var translate = new TranslateTransform();
                    var rotate = new RotateTransform();
                    var transGroup = new TransformGroup();
                    transGroup.Children.Add(translate);
                    transGroup.Children.Add(rotate);
                    view.RenderTransform = transGroup;
                    view.SetBinding(TranslateTransform.XProperty, BindingHelper.Create("XPosition", ctx));
                    view.SetBinding(TranslateTransform.YProperty, BindingHelper.Create("YPosition", ctx));
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}


//<ItemsControl.ItemTemplate>
//    <DataTemplate DataType="{x:Type creeps2:Type1Variant1}">
//        <creeps3:Type1Variant1View
//            Visibility="{Binding IsAlive, Converter={StaticResource boolToVis},
//                UpdateSourceTrigger=PropertyChanged, 
//                NotifyOnSourceUpdated=True,
//                NotifyOnTargetUpdated=True}">
//            <creeps3:Type1Variant1View.RenderTransform>
//                <TranslateTransform X="{Binding XPosition}"
//                                    Y="{Binding YPosition}"/>
//            </creeps3:Type1Variant1View.RenderTransform>
//        </creeps3:Type1Variant1View>
//    </DataTemplate>
//</ItemsControl.ItemTemplate>