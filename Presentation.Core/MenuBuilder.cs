using System;
using System.Collections.Generic;
using Hdd.Contract;
using Hdd.ModuleLoader;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public static class MenuBuilder
    {
        public static Menu.Core.Menu CreateMenu(IEnumerable<Lazy<IModuleContract>> modules)
        {
            var menuCompositionHelper = new CompositionHelper<IModuleCommand, IMenuGroupItemAttribute>();
            menuCompositionHelper.AssembleModuleComponents(modules);

            var menu = new Menu.Core.Menu();
            foreach (var moduleCommand in menuCompositionHelper.Modules)
            {
                menu.Parse(moduleCommand.Metadata.Items, moduleCommand.Value);
            }
            menu.AddSeparators();

            return menu;
        }
    }
}