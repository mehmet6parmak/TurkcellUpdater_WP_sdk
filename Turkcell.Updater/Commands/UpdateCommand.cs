using System;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using Turkcell.Updater.Utility;
using Windows.System;

namespace Turkcell.Updater.Commands
{
    internal class UpdateCommand : ICommand
    {
        private readonly Update _update;
        private readonly UpdaterDialogManager _manager;

        public UpdateCommand(UpdaterDialogManager manager, Update update)
        {
            _manager = manager;
            _update = update;
        }

        public bool CanExecute(object parameter)
        {
            return _update != null;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            if (!String.IsNullOrEmpty(_update.TargetPackageId) && !PackageUtility.IsInstalled(_update.TargetPackageId, _update.TargetVersion))
            {                
                LaunchMarketplace();
                OnExecuted();
            }
            else if (_update.TargetAppUriSchema != null)
            {
                await Launcher.LaunchUriAsync(_update.TargetAppUriSchema);
                OnExecuted();
            }
            else if (_update.TargetWebSiteUrl != null)
            {
                await Launcher.LaunchUriAsync(_update.TargetWebSiteUrl);
                OnExecuted();
            }
        }

        private void LaunchMarketplace()
        {            
            var task = new MarketplaceDetailTask
                {
                    ContentType = MarketplaceContentType.Applications,
                    ContentIdentifier = _update.TargetPackageId
                };
            task.Show();            
        }

        private void OnExecuted()
        {
            if (_update.ForceExit || _update.ForceUpdate)
                _manager.FireShouldExitApplication();
            else
                _manager.FireOnCompleted();
        }
    }
}
