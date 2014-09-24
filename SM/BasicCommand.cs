using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SM
{
    public class BasicCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public BasicCommand(Action<object> onExecuteMethod, Func<object, bool> onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public BasicCommand(Action onExecuteMethod, Func<bool> onCanExecuteMethod)
        {
            _execute = p => onExecuteMethod();
            _canExecute = p =>  onCanExecuteMethod();
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }


    }
}
