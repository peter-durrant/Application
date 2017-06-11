using System;
using System.Collections.Generic;
using Hdd.Contract;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel(IEnumerable<IMenuCommand> coreCommands, IEnumerable<Lazy<IModuleContract>> modules)
        {
            var logger = new Logger.Logger();
            logger.Info(this, "Load menus");
            Menu = MenuBuilder.CreateMenu(coreCommands, modules);
        }

        public Menu.Core.Menu Menu { get; }
    }
}