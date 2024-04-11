using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blumen.Models
{
    internal class RelayCommand : ICommand
    {
        Action<object> _executeMethod;
        Func<object, bool> _canExecuteMethod;

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
