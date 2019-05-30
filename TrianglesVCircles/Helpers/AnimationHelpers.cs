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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TrianglesVCircles.GameControls.Creeps;

namespace TrianglesVCircles.Helpers
{
    public static class AnimationHelpers
    {
        static Random _rand = new Random(GlobalRandom.Next(891, 1699));

        public static void PlayAffectBG(this FrameworkElement src,
            string sbName)
        {
            var sb = src.Resources[sbName] as Storyboard;
            if (!ReferenceEquals(null, sb))
            {
                sb.AutoReverse = true;
                sb.RepeatBehavior = RepeatBehavior.Forever;
                sb.Begin();
            }
        }

        public static void PlayForeverAndRandomize(
            this FrameworkElement src, string key)
        {
            var anim = src.Resources[key] as Storyboard;
            anim.RepeatBehavior = RepeatBehavior.Forever;


            var delayOffset = _rand.Next(81) * 10;
            Timed.Exec(() => anim.Dispatcher.Invoke(anim.Begin), delayOffset);
        }

        public static void AnimateDestroyed(this BaseEnemyControl src)
        {
            var ease = new PowerEase
            {
                EasingMode = EasingMode.EaseOut,
                Power = 3.78,
            };

            var oEase = new PowerEase
            {
                EasingMode = EasingMode.EaseOut,
                Power = 4.97,
            };


            var opacAnim = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1200),
                EasingFunction = oEase
            };

            var transYAnim = new DoubleAnimation
            {
                To = -_rand.Next(5, 40),
                Duration = TimeSpan.FromMilliseconds(950),
                EasingFunction = ease
            };

            var transXAnim = new DoubleAnimation
            {
                To = _rand.Next(0, 20),
                Duration = TimeSpan.FromMilliseconds(950),
                EasingFunction = ease
            };

            var widthAnim = new DoubleAnimation
            {
                From = src.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1200),
                EasingFunction = ease
            };

            var heightAnim = new DoubleAnimation
            {
                From = src.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(950),
                EasingFunction = new BackEase
                {
                    Amplitude = 1.25,
                    EasingMode = EasingMode.EaseOut
                }
            };

            var trans = new TranslateTransform();


            src.Dispatcher.Invoke(() =>
            {
                src.RenderTransform = trans;
                src.BeginAnimation(
                    UIElement.OpacityProperty,
                    opacAnim);
                trans.BeginAnimation(
                    TranslateTransform.YProperty,
                    transYAnim);
                trans.BeginAnimation(
                    TranslateTransform.XProperty,
                    transXAnim);
                src.BeginAnimation(
                    FrameworkElement.WidthProperty,
                    widthAnim);
                src.BeginAnimation(
                    FrameworkElement.HeightProperty,
                    heightAnim);
            });
        }

        public static void AnimateEnemyIn(this BaseEnemyControl src)
        {
            src.Model.Invulnerable = true;
            var trans = new TranslateTransform();

            var duration = TimeSpan.FromMilliseconds(
                _rand.Next(9, 30) * 100);

            var comp = 350 - src.YPosition;
            var yAnim = new DoubleAnimation
            {
                From = -(src.YPosition + comp),
                EasingFunction = new PowerEase
                {
                    EasingMode = EasingMode.EaseInOut,
                    Power = 3.824,
                },
                To = 0,
                Duration = duration,
                AutoReverse = false,
            };

            yAnim.Completed += (sender, args) =>
            {
                src.Model.Invulnerable = false;
                src.Model.Attack.Begin();
                src.Model.RandomMover.Begin();
            };

            src.RenderTransform = trans;
            trans.BeginAnimation(TranslateTransform.YProperty, yAnim);

        }
    }


    public static class Timed
    {
        public static async void Exec(Action action, int timeoutInMilliseconds)
        {
            await Task.Delay(timeoutInMilliseconds);
            action();
        }
    }
}
