using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ColorsHelper
{
    internal sealed class BasicCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public BasicCommand(Action execute)
            : this(execute, () => true)
        {
        }

        public BasicCommand(Action<object> execute)
            : this(execute, o => true)
        { }

        public BasicCommand(Action execute, Func<bool> canExecute)
            : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            if (canExecute == null)
                throw new ArgumentNullException("canExecute");
        }

        public BasicCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            if (canExecute == null)
                throw new ArgumentNullException("canExecute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CanExecuteChanged += value; }
            remove { CanExecuteChanged -= value; }
        }

        void ICommand.Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

        private event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
