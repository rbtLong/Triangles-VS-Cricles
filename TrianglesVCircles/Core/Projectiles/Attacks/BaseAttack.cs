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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TrianglesVCircles.Annotations;
using TrianglesVCircles.Core.Animation;
using TrianglesVCircles.Core.Projectiles.EnemyProjectiles;
using TrianglesVCircles.Helpers;
using Timer = System.Timers.Timer;

namespace TrianglesVCircles.Core.Projectiles.Attacks
{
    public abstract class BaseAttack
    {
        private readonly Timer _refresher;
        private TimeSpan _frequency = TimeSpan.FromMilliseconds(600);
        private IAttackable _source;
        private IAttackable _target;
        private readonly ObservableImmutableList<TweenAnimator> _animators = new ObservableImmutableList<TweenAnimator>(); 
        private ObservableImmutableList<IProjectile> _projectiles = new ObservableImmutableList<IProjectile>();
        private TimeSpan _attackSpeed = TimeSpan.FromMilliseconds(6500);
        protected readonly SynchronizationContext _synch;
        private int _damage = 1;
        private bool _singleAttack = true;
        private Random _random = new Random(GlobalRandom.Next(53,316));

        public bool SingleAttack
        {
            get { return _singleAttack; }
            set { _singleAttack = value; }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        public event EventHandler<IProjectile> Added = delegate { };
        public event EventHandler<IProjectile> Removed = delegate { };
        public event EventHandler<IProjectile> Hit = delegate { };
        public event EventHandler<IProjectile> Expired = delegate { }; 


        public TimeSpan AttackSpeed
        {
            get { return _attackSpeed; }
            set
            {
                _refresher.Interval = getRefresherInterval();
                _attackSpeed = value;
                OnPropertyChanged();
            }
        }

        public ObservableImmutableList<IProjectile> Projectiles
        {
            get { return _projectiles; }
            set
            {
                if (Equals(value, _projectiles)) return;
                _projectiles = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Frequency
        {
            get { return _frequency; }
            set
            {
                if (value.Equals(_frequency)) return;
                _frequency = value;
                OnPropertyChanged();
            }
        }

        public IAttackable Source
        {
            get { return _source; }
            set
            {
                if (Equals(value, _source)) return;
                _source = value;
                OnPropertyChanged();
            }
        }

        public IAttackable Target
        {
            get { return _target; }
            protected set
            {
                if (Equals(value, _target)) return;
                _target = value;
                OnPropertyChanged();
            }
        }

        public abstract IEnumerable<BaseEnemyProjectile> OnNewProjectile();

        protected BaseAttack(IAttackable src, IAttackable target)
        {
            _synch = SynchronizationContext.Current;
            _source = src;
            _target = target;
            _refresher = new Timer(_frequency.TotalMilliseconds + 
                getRefresherInterval());
            _refresher.Elapsed += _refresher_Elapsed;
        }

            private int getRefresherInterval()
            {
                var p = _random.Next(
                    (int) (_attackSpeed.TotalMilliseconds*.45),
                    (int) _attackSpeed.TotalMilliseconds);
                if (!SingleAttack) p = (int)(_frequency.TotalMilliseconds + (_random.Next(0,8)*100));
                return p;
            }

            void _refresher_Elapsed(object sender, ElapsedEventArgs e)
            {
                addProjectileAndAnimate();
            }

                private void addProjectileAndAnimate()
                {
                    foreach (var p in OnNewProjectile())
                    {
                        p.Damage = _damage;

                        p.OnExpired += (sender, args) =>
                            _synch.Post(d =>
                            {
                                Expired(this, p);
                                removeProjectileAndAnimation(p);
                            }, null);

                        p.Hit += (sender, arg) => Task.Run(() =>
                        {
                            var projectile = p;
                            Hit(this, projectile);
                            p.Expired = true;
                        });

                        var anim = new TweenAnimator(p, _attackSpeed);
                        anim.Completed += (os, m) => removeProjectileAndAnimation(m);
                        anim.Completed += (sender, movable) => anim.Dispose();
                        _animators.Add(anim);
                        anim.Begin();

                        p.PropertyChanged += (o, args) =>
                        {
                            if (!p.OnStage)
                                removeProjectileAndAnimation(p);
                        };

                        _projectiles.DoAdd(o => p);
                        Added(this, p);
                    }

                }

                    private void removeProjectileAndAnimation(IMovable p)
                    {
                        if (!_projectiles.Contains(p)) return;
                        try
                        {
                            _projectiles.DoRemove(o => p as IProjectile);
                            if (_animators.Any(ob => ob.Item.Equals(p)))
                                _animators.DoRemove(o =>
                                    _animators.FirstOrDefault(ob => ob.Item.Equals(p)));
                        }
                        catch { }

                        Removed(this, p as IProjectile);
                    }

        public void Begin()
        {
            if (_refresher.Enabled) return;
            _refresher.Enabled = true;

            //initial animation in takes time
            addProjectileAndAnimate();

        }

        public void Pause()
        {
            _refresher.Enabled = false;
            foreach(var o in _animators)
                o.Pause();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            foreach (var o in _animators)
                o.Dispose();
            foreach (var p in Projectiles)
            {
                p.Expired = true;
                Projectiles.DoRemove(o => p);
            }

            _refresher.Stop();
            _animators.Clear();
            _projectiles.Clear();
        }

        public void Continue()
        {
            _refresher.Enabled = true;
            foreach (var o in _animators)
                o.Continue();
        }
    }
}
