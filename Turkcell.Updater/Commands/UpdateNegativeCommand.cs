using System;
using System.Windows.Input;

namespace Turkcell.Updater.Commands
{
    internal class UpdateNegativeCommand : ICommand
    {
        private readonly UpdaterDialogManager _manager;
        private readonly Update _update;

        public UpdateNegativeCommand(Update update, UpdaterDialogManager manager)
        {
            _update = update;
            _manager = manager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_update.ForceExit || _update.ForceUpdate)
            {
                _manager.FireShouldExitApplication();
            }
            else
            {
                _manager.FireOnCompleted();
            }
        }
    }
}