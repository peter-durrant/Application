using System;
using System.Collections.Generic;
using Hdd.Contract;
using Hdd.ModuleLoader;

namespace Hdd.Presentation.Core
{
    public class MainMenuViewModel : ViewModelBase
    {
        private readonly CompositionHelper<IModuleCommand, IModuleLocationAttribute> _menuCompositionHelper;

        public MainMenuViewModel(IEnumerable<Lazy<IModuleContract>> modules)
        {
            // create logger
            var logger = new Logger.Logger();

            // load menu modules
            logger.Info(this, "Load menus");

            _menuCompositionHelper = new CompositionHelper<IModuleCommand, IModuleLocationAttribute>();
            _menuCompositionHelper.AssembleModuleComponents(modules);

            MenuGroups = new MenuGroups {Groups = new Dictionary<Tuple<int, string>, MenuItem>()};
            foreach (var moduleCommand in MenuModuleCommands)
            {
                var groupIndex = MainMenuLocations.Position(moduleCommand.Metadata.GroupName);
                var groupName = new Tuple<int, string>(groupIndex, moduleCommand.Metadata.GroupName);
                if (!MenuGroups.Groups.ContainsKey(groupName))
                {
                    var moduleLocationName = new ModuleLocationName(moduleCommand.Value.Module);

                    var menuItem = new MenuItem
                    {
                        Name = moduleLocationName.ModuleLocation(groupName.Item2),
                        ChildItems = new HashSet<MenuItem>()
                    };
                    MenuGroups.Groups[groupName] = menuItem;
                }
                MenuGroups.Groups[groupName].ChildItems.Add(new MenuItem(moduleCommand.Value));
            }
        }

        private IEnumerable<Lazy<IModuleCommand, IModuleLocationAttribute>> MenuModuleCommands
            => _menuCompositionHelper.Modules;

        public MenuGroups MenuGroups { get; }
    }
}