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
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Problems.MathRelated
{
    public class MathProblem
    {
        private Random _random = new Random(GlobalRandom.Next(491, 680));
        public int Min { get; set; }
        public int Max { get; set; }

        public MathProblem(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public Tuple<string, string> AddProblem()
        {
            var question = "";
            var answer = "";

            var arg1 = _random.Next(Min, Max);
            var arg2 = _random.Next(Min, Max);
            question = string.Format("{0} + {1}", arg1, arg2);
            answer = (arg1 + arg2).ToString();
            return Tuple.Create(question, answer);
        }

        public Tuple<string, string> SubtractProblem()
        {
            var question = "";
            var answer = "";

            var arg1 = _random.Next(Min, Max);
            var arg2 = _random.Next(arg1, Max) * _random.Next(1, 3);
            question = string.Format("{0} - {1}", arg2, arg1);
            answer = (arg2-arg1).ToString();
            return Tuple.Create(question, answer);
        }

        public Tuple<string, string> MultiplicationProblem()
        {
            var question = "";
            var answer = "";

            var arg1 = _random.Next(Min, Max);
            var arg2 = _random.Next(Min, Max);
            question = string.Format("{0} * {1}", arg1, arg2);
            answer = (arg1 * arg2).ToString();
            return Tuple.Create(question, answer);
        }

        public Tuple<string, string> DivisionProblem()
        {
            var question = "";
            var answer = "";

            var arg1 = _random.Next(Min, Max);
            var arg2 = _random.Next(Min, Max);
            var product = arg1*arg2;
            question = string.Format("{0} / {1}", product, arg1);
            answer = arg2.ToString();
            return Tuple.Create(question, answer);
        }

        public Tuple<string, string> GetRandomProblem()
        {
            var p = Tuple.Create("1+1", "2");
            switch (_random.Next(0, 4))
            {
                case 0:
                    p = AddProblem();
                    break;
                case 1:
                    p = SubtractProblem();
                    break;
                case 2:
                    p = MultiplicationProblem();
                    break;
                case 3:
                    p = DivisionProblem();
                    break;

            }
            return p;
        }

        public Tuple<string, string> DerivativeProblem()
        {
            var question = "";
            var answer = "";

            var arg1 = _random.Next(Min, Max);
            var arg2 = _random.Next(Min, Max);
            var product = arg1 * arg2;
            question = string.Format("{0} / {1}", product, arg1);
            answer = arg2.ToString();
            return Tuple.Create(question, answer);
        }

    }
}
