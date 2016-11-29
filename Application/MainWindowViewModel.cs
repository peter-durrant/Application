using System;
using System.Collections.Generic;
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
         _logger.Info(this, "Load modules");
         _compositionHelper = new CompositionHelper<IModuleContract>();
         _compositionHelper.AssembleModuleComponents();
      }

      public IEnumerable<Lazy<IModuleContract>> Modules => _compositionHelper.Modules;
   }
}