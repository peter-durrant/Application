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
      }

      public IEnumerable<IModuleCommand> MenuModules
      {
         get { return _menuCompositionHelper.Modules.Select(x => x.Value); }
      }
   }
}