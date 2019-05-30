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
using System.Runtime.CompilerServices;
using TrianglesVCircles.Annotations;

namespace TrianglesVCircles.Core.Penalties
{
    public abstract class BaseAffect : INotifyPropertyChanged
    {
        private TimeSpan _duration;
        protected DateTime _acquired = DateTime.Now;
        protected double _scoreDelta;
        protected int _level;
        protected int _consecutive = 1;
        private bool _expired;


        public bool Expired
        {
            get { return _expired; }
            set
            {
                if (value.Equals(_expired)) return;
                _expired = value;
                if (Expired) AffectFinished(this, null);
                OnPropertyChanged();
            }
        }

        public event EventHandler AffectFinished = delegate { };

        public int Level { get { return _level; } }

        public double ScoreDelta { get { return _scoreDelta; } }

        public abstract string Name { get; }

        public TimeSpan Duration
        {
            get { return _duration; }
            protected set
            {
                if (value.Equals(_duration)) return;
                _duration = value;
            }
        }

        public int Consecutive { get { return _consecutive; } }


        public void IncrementConsec()
        {
            _consecutive++;
            OnPropertyChanged("Duration");
            OnPropertyChanged("Consecutive");
            OnPropertyChanged("ScoreDelta");
        }

        public DateTime Acquired
        {
            get { return _acquired; }
        }

        protected BaseAffect(TimeSpan duration)
        {
            _duration = duration;
        }

        protected BaseAffect()
        {
        }

        public abstract void ExtendDuration();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
