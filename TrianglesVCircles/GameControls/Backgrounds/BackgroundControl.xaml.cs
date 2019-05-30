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

using System.Windows.Controls;

namespace TrianglesVCircles.GameControls.Backgrounds
{
	/// <summary>
	/// Interaction logic for BackgroundControl.xaml
	/// </summary>
	public partial class BackgroundControl : UserControl
	{
	    public static BackgroundControl Instance { get; private set; }

	    public int CurrentLevel { get; private set; }

	    public BackgroundControl()
		{
			InitializeComponent();
		    Instance = this;
            Loaded += BackgroundControl_Loaded;
		}

        void BackgroundControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadLevel(-1);
            Play();
        }

	        public void Play()
	        {
                if(MainBg.Content is IBackground)
                    (MainBg.Content as IBackground).Play();
	        }

	        public void Stop()
	        {
                if (MainBg.Content is IBackground)
                    (MainBg.Content as IBackground).Stop();
	        }

	        public void Pause()
	        {
                if (MainBg.Content is IBackground)
                    (MainBg.Content as IBackground).Pause();
	        }

        /* CAUTION!
         * When creating a new background, name the
         * main animation storyboard "Animation".
         */
	    public void LoadLevel(int e)
	    {
	        if (e == CurrentLevel) return;
	        CurrentLevel = e;

	        switch (e)
	        {
                case -1:
	                MainBg.Content = new MainMenu();
	                break;

                case 0:
	                MainBg.Content = new Level0();
	                break;
	        }

	    }

	}
}