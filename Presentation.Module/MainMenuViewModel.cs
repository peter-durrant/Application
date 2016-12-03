using System.Collections.Generic;
using System.Linq;
using Hdd.Module1;
using Hdd.ModuleLoader;

namespace Hdd.Presentation.Module
{
   public class MainMenuViewModel
   {
      private readonly CompositionHelper<IModuleCommand, IModuleLocationAttribute> _menuCompositionHelper;

      public MainMenuViewModel()
      {
         _menuCompositionHelper = new CompositionHelper<IModuleCommand, IModuleLocationAttribute>();
         _menuCompositionHelper.AssembleModuleComponents();
         var module = new Module1Module();
         foreach (var moduleCommand in MenuModules)
         {
            moduleCommand.Module = module;
         }
      }

      public IEnumerable<IModuleCommand> MenuModules
      {
         get { return _menuCompositionHelper.Modules.Select(x => x.Value); }
      }
   }
}