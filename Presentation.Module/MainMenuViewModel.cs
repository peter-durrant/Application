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
         _menuCompositionHelper.AssembleModuleComponents();

         ConnectCommandToModule(modules.ToList());
      }

      public IEnumerable<IModuleCommand> MenuModules
      {
         get { return _menuCompositionHelper.Modules.Select(x => x.Value); }
      }

      // TODO is this a hack? The commands in Module<X>.Presentation need to refer to the implentation in Module<X>.
      // TODO is there anyway to do this directly (or better)?
      private void ConnectCommandToModule(IList<Lazy<IModuleContract>> modules)
      {
         foreach (var moduleCommand in MenuModules)
         {
            var presentationNamespace = moduleCommand.GetType().Namespace;
            foreach (var module in modules)
            {
               if (presentationNamespace.StartsWith(module.Value.GetType().Namespace))
               {
                  moduleCommand.Module = module.Value;
                  break;
               }
            }
         }
      }
   }
}