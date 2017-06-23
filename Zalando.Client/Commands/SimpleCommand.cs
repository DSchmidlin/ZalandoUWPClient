using System;
using System.Windows.Input;

namespace Zalando.Client.Commands
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _actionToExecute;
        private readonly Func<bool> _canExecute;

        public SimpleCommand(Action actionToExecute, Func< bool> canExecute)
        {
            _actionToExecute = actionToExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _actionToExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}
