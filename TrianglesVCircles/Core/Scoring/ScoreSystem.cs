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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core.Scoring
{
    public class ScoreSystem : INotifyPropertyChanged
    {
        private double _totalScore;
        private readonly ObservableImmutableList<ScoreEntry> _scores 
            = new ObservableImmutableList<ScoreEntry>();

        public event EventHandler<ScoreEntry> PointDeducted = delegate { };
        public event EventHandler<ScoreEntry> PointAdded = delegate { }; 

        public ObservableImmutableList<ScoreEntry> Scores
        {
            get { return _scores; }
        }

        public double TotalScore
        {
            get { return _totalScore; }
            set
            {
                if (value.Equals(_totalScore)) return;
                _totalScore = value;
                OnPropertyChanged();
            }
        }

        public ScoreSystem()
        {
            Scores.CollectionChanged += (sender, args) =>
            {
                if (_scores.Any())
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        var last = _scores.LastOrDefault();
                        if(last.Delta > 0) 
                            PointAdded(this, _scores.LastOrDefault());
                        else if (last.Delta < 0) 
                            PointDeducted(this, _scores.LastOrDefault());

                    }
                }
                recalculateTotal();
            };
        }

            private void recalculateTotal()
            {
                TotalScore = _scores.Sum(o => o.Delta);
                calculateConsecutiveBonus();
            }

                private void calculateConsecutiveBonus()
                {
                    for (var i = 0; i < _scores.Count; ++i)
                    {
                        if (i + 1 <= _scores.Count - 1
                            && _scores[i].Reason == _scores[i+1].Reason)
                        {

                            var delayed = _scores[i + 1].Issued
                                .Subtract(_scores[i].Issued);
                            var cap = TimeSpan.FromSeconds(5);
                            var multiplier = (cap.TotalMilliseconds/delayed.TotalMilliseconds)/100;
                            if (_scores[i].Delta > 0)
                            {
                                if (Math.Abs(delayed.Ticks) < 0.5) 
                                    TotalScore += 5000.0;
                                else 
                                    TotalScore += (_scores[i].Delta * .80)
                                        * multiplier;
                            }
                            else
                            {
                                if (Math.Abs(delayed.Ticks) < 0.5)
                                    TotalScore -= 3000.0;
                                else
                                    TotalScore += (_scores[i].Delta * 1.20)
                                        * (cap.TotalMilliseconds / delayed.TotalMilliseconds);
                            }
                        }
                    }
                }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
