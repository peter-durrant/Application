using System;
using System.Collections.Generic;
using System.Linq;
using Hdd.Logger;
using Hdd.ModuleContract;
using Hdd.ModuleLoader;
using Hdd.Presentation.Core;

namespace Hdd.Application
{
   public class MainWindowViewModel : ViewModelBase
   {
      private readonly CompositionHelper<IModuleContract> _compositionHelper;
      private readonly ILogger _logger;

      public MainWindowViewModel()
      {
         // create logger
         _logger = new Logger.Logger();

         // load modules
         _compositionHelper = new CompositionHelper<IModuleContract>();
         _compositionHelper.AssembleModuleComponents();

         // log module name and version
         var modules = Modules.ToList();
         _logger.Info(this,
            "Loaded modules: " + string.Join(" ", modules.Select(x => $"{x.Value.Name} ({x.Value.Version})")));
      }

      public IEnumerable<Lazy<IModuleContract>> Modules => _compositionHelper.Modules;
   }
}