using System;
using System.Collections.Generic;
using Hdd.Contract;
using Hdd.ModuleLoader;
using HDD.Utility;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public static class MenuBuilder
    {
        public static Menu.Core.Menu CreateMenu(IEnumerable<IMenuCommand> coreCommands, IEnumerable<Lazy<IModuleContract>> modules)
        {
            var menuCompositionHelper = new CompositionHelper<IModuleCommand, IMenuGroupItemAttribute>();
            menuCompositionHelper.AssembleModuleComponents(modules);

            var menu = new Menu.Core.Menu();
            foreach (var coreCommand in coreCommands)
            {
                menu.Parse(coreCommand.GetType().GetAttributeValue((MenuGroupItemAttribute attribute) => attribute.Items), coreCommand);
            }
            foreach (var moduleCommand in menuCompositionHelper.Modules)
            {
                menu.Parse(moduleCommand.Metadata.Items, moduleCommand.Value);
            }
            menu.AddSeparators();
            menu.ActivateParents();

            return menu;
        }
    }
}