using System;
using System.Windows.Input;

namespace Turkcell.Updater.Commands
{
    internal class MessageCommand : ICommand
    {
        private readonly Action _executeAction;

        /// <summary>
        /// </summary>
        /// <param name="executeAction"></param>
        public MessageCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeAction();
        }
    }
}