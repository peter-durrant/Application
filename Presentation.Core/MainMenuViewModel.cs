using System;
using System.Collections.Generic;
using Hdd.Contract;

namespace Hdd.Presentation.Core
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel(IEnumerable<Lazy<IModuleContract>> modules)
        {
            var logger = new Logger.Logger();
            logger.Info(this, "Load menus");
            Menu = MenuBuilder.CreateMenu(modules);
        }

        public Menu.Core.Menu Menu { get; }
    }
}