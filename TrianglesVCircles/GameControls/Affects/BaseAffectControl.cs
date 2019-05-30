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
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TrianglesVCircles.Core.Affects;

namespace TrianglesVCircles.GameControls.Affects
{
    public class BaseAffectControl : UserControl, IDisposable
    {
        private Timer _refresher;
        private WrongButtonAffect _affect;

        public static readonly DependencyProperty TimeRemainingProperty = 
            DependencyProperty.Register(
                "TimeRemaining", typeof (string), 
                typeof (BaseAffectControl), 
                new PropertyMetadata(default(string)));

        public string TimeRemaining
        {
            get { return (string) GetValue(TimeRemainingProperty); }
            set { SetValue(TimeRemainingProperty, value); }
        }

        public BaseAffectControl()
        {
            TimeRemaining = "";
            DataContextChanged += WrongButtonAffectControl_DataContextChanged;
            _refresher = new Timer(refresherLapsed, null, 0, 300);
        }

            private void refresherLapsed(object state)
            {
                if (ReferenceEquals(_affect, null))
                {
                    object dc = null;
                    Dispatcher.Invoke(() => dc = DataContext);
                    if (dc is WrongButtonAffect)
                    {
                        var affect = dc as WrongButtonAffect;
                        _affect = affect;
                        _affect.PropertyChanged += AffectOnPropertyChanged;
                    }
                    else return;
                }

                WrongButtonAffect fx = null;
                Dispatcher.Invoke(() => fx = _affect);
                if (DateTime.Now.Subtract(fx.Acquired) > fx.Duration)
                {
                    Dispatcher.Invoke(() => fx.Expired = true);

                    if (_refresher != null)
                    {
                        _refresher.Dispose();
                        _refresher = null;
                    }
                    return;
                }

                Dispatcher.Invoke(() =>
                {
                    TimeRemaining = (_affect.Duration - DateTime.Now.Subtract(_affect.Acquired))
                        .TotalSeconds.ToString("00", CultureInfo.InvariantCulture);
                });
            }

                private void AffectOnPropertyChanged(
                    object sender, PropertyChangedEventArgs e)
                {
                    if (e.PropertyName == "Duration")
                    {

                    }
                }

        void WrongButtonAffectControl_DataContextChanged(object sender, 
            DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is WrongButtonAffect)
            {
                var affect = e.NewValue as WrongButtonAffect;
                _affect = affect;
            }
        }

        public void Dispose()
        {
            if (_refresher != null)
            {
                _refresher.Dispose();
                _refresher = null;
            }
        }
    }
}
