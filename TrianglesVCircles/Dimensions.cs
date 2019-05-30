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

namespace TrianglesVCircles
{
    public static class Dimensions
    {
        private static double _shipWidth = 65;
        private static double _shipHeight = 65;
        private static double _stageHeight = 800;
        private static double _stageWidth = 600;
        private static double _bottomStageDetailHeight = 50;
        public static double _windowBorder = 2.5;

        static double _enemyHeight = 30;
        static double _enemyWidth = 30;

        public static Rect StageBoundary
        {
            get { return new Rect(new Size(_stageWidth, _stageHeight)); }
        }

        public static double EnemyHeight 
        {
            get { return _enemyHeight; }
            set { _enemyHeight = value; }
        }

        public static double EnemyWidth
        {
            get { return _enemyWidth; }
            set { _enemyWidth = value;}
        }


        public static double StageWidth
        {
            get { return _stageWidth; }
            set { _stageWidth = value; }
        }

        public static double StageHeight
        {
            get { return _stageHeight; }
            set { _stageHeight = value; }
        }

        public static double BottomStageDetailHeight
        {
            get { return _bottomStageDetailHeight; }
            set { _bottomStageDetailHeight = value; }
        }

        public static double ShipWidth
        {
            get { return _shipWidth; }
            set { _shipWidth = value; }
        }

        public static double ShipHeight
        {
            get { return _shipHeight; }
            set { _shipHeight = value; }
        }

        public static double WindowHeight
        {
            get
            {
                return StageHeight + BottomStageDetailHeight 
                    + WindowBorderThickness.Left
                    + WindowBorderThickness.Right;
            }
        }

        public static double WindowWidth
        {
            get
            {
                return StageWidth 
                    + WindowBorderThickness.Top 
                    + WindowBorderThickness.Bottom;
            }
        }

        public static Point StageCenter
        {
            get { return new Point(_stageWidth/2.0,_stageHeight/2.0); }
        }

        public static Thickness WindowBorderThickness
        {
            get { return new Thickness(_windowBorder); }
        }
    }
}
