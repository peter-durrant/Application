using System;
using System.Collections.Generic;
using System.Linq;
using Hdd.Contract;
using Hdd.Logger;
using Hdd.ModuleLoader;
using Hdd.Presentation.Core;

namespace Hdd.Presentation.Module
{
   public class MainMenuViewModel : ViewModelBase
   {
      private readonly ILogger _logger;
      private readonly CompositionHelper<IModuleCommand, IModuleLocationAttribute> _menuCompositionHelper;

      public MainMenuViewModel(IEnumerable<Lazy<IModuleContract>> modules)
      {
         // create logger
         _logger = new Logger.Logger();

         // load menu modules
         _logger.Info(this, "Load menus");

         _menuCompositionHelper = new CompositionHelper<IModuleCommand, IModuleLocationAttribute>();
         _menuCompositionHelper.AssembleModuleComponents(modules);

         MenuGroups = new MenuGroups {Groups = new Dictionary<string, MenuItem>()};
         foreach (var moduleCommand in MenuModuleCommands)
         {
            var groupName = moduleCommand.Metadata.GroupName;
            if (!MenuGroups.Groups.ContainsKey(groupName))
            {
               var moduleLocationName = new ModuleLocationName(moduleCommand.Value.Module);

               var menuItem = new MenuItem
               {
                  Name = moduleLocationName.ModuleLocation(groupName),
                  ChildItems = new List<MenuItem>()
               };
               MenuGroups.Groups[groupName] = menuItem;
            }
            MenuGroups.Groups[groupName].ChildItems.Add(new MenuItem(moduleCommand.Value));
         }
      }

      public IEnumerable<IModuleCommand> MenuModules
      {
         get { return _menuCompositionHelper.Modules.Select(x => x.Value); }
      }

      public IEnumerable<Lazy<IModuleCommand, IModuleLocationAttribute>> MenuModuleCommands
         => _menuCompositionHelper.Modules;

      public MenuGroups MenuGroups { get; set; }
   }
}