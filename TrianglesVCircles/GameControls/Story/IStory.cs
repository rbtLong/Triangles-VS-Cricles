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
using System.Windows.Media.Animation;
using TrianglesVCircles.GameControls.Sound;

namespace TrianglesVCircles.GameControls.Story
{
    public interface IStory
    {
        Storyboard Animation { get; }
        string Music { get; }
        event EventHandler Completed;
        ResourceDictionary Resources { get; }
    }

    public static class StoryExtensionMethods
    {
        public static void Play(this IStory src)
        {
            SoundControl.Instance.LoadSource(src.Music);
            SoundControl.Instance.Play();
            src.Animation.Begin();
        }

        public static void Pause(this IStory src)
        {
            SoundControl.Instance.Pause();
            src.Animation.Pause();
        }

        public static void Stop(this IStory src)
        {
            SoundControl.Instance.Stop();
            src.Animation.Stop();
        }

        public static Storyboard GetAnimation(this IStory src)
        {
            return src.Resources["Animation"] as Storyboard;
        }

        public static void SubscribeCompletion(this IStory src, Action act)
        {
            GetAnimation(src).Completed +=
                (sender, args) => act.Invoke();
        }


    }
}
