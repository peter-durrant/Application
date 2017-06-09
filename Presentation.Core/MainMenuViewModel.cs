using System;
using System.Collections.Generic;
using Hdd.Contract;
using Hdd.ModuleLoader;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly CompositionHelper<IModuleCommand, IMenuGroupItemAttribute> _menuCompositionHelper;

        public MainMenuViewModel(IEnumerable<Lazy<IModuleContract>> modules)
        {
            // create logger
            var logger = new Logger.Logger();

            // load menu modules
            logger.Info(this, "Load menus");

            _menuCompositionHelper = new CompositionHelper<IModuleCommand, IMenuGroupItemAttribute>();
            _menuCompositionHelper.AssembleModuleComponents(modules);

            Menu = new Menu.Core.Menu();
            foreach (var moduleCommand in MenuModuleCommands)
            {
                Menu.Parse(moduleCommand.Metadata.Items, moduleCommand.Value);
            }
        }

        private IEnumerable<Lazy<IModuleCommand, IMenuGroupItemAttribute>> MenuModuleCommands
            => _menuCompositionHelper.Modules;

        public Menu.Core.Menu Menu { get; }
    }
}