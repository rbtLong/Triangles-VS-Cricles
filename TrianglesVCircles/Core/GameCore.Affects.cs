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
using System.Linq;
using TrianglesVCircles.Core.Penalties;
using TrianglesVCircles.Helpers;

namespace TrianglesVCircles.Core
{
    partial class GameCore
    {
        public event EventHandler<BaseAffect> AffectAdded = delegate { };
        public event EventHandler<BaseAffect> AffectRemoved = delegate { }; 

        private readonly ObservableImmutableList<BaseAffect> _shipAffects
            = new ObservableImmutableList<BaseAffect>();

        public ObservableImmutableList<BaseAffect> ShipAffects
        {
            get { return _shipAffects; }
        }

        private void initAffects()
        {
            ShipAffects.CollectionChanged += ShipAffects_CollectionChanged;
        }

        private void ShipAffects_CollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var fx = _shipAffects.LastOrDefault();
                fx.AffectFinished += (o, args) => 
                    synch.Post(d =>
                    {
                        _shipAffects.Remove(fx);
                        AffectRemoved(this, fx);
                    }, null);
                AffectAdded(this, fx);
            }
        }
    }
}
