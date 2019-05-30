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
using System.Windows.Input;

namespace TrianglesVCircles.Helpers
{

    public class CommandHandler : ICommand
    {
        private readonly Action<object> execAction;
        public Func<object, bool> canExecFunc;

        public CommandHandler(Action<object> execAction)
        {
            this.execAction = execAction;
        }

        public CommandHandler(Action<object> execAction, Func<object, bool> canExecFunc)
        {
            this.execAction = execAction;
            this.canExecFunc = canExecFunc;
            CanExecuteChanged += CommandHandler_CanExecuteChanged;
        }

        private void CommandHandler_CanExecuteChanged(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            if (canExecFunc != null)
                return canExecFunc.Invoke(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            if (execAction != null)
                execAction.Invoke(parameter);
        }


        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                foreach (var v in CanExecuteChanged.GetInvocationList())
                {
                    try
                    {
                        v.DynamicInvoke(this, new EventArgs());
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

}
