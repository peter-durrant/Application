using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Logger;
using Hdd.ModuleLoader;
using Hdd.Presentation.Core;
using Hdd.Presentation.Module;

namespace Hdd.Application
{
   public class MainWindowViewModel : ViewModelBase
   {
      private readonly CompositionHelper<IModuleContract> _compositionHelper;
      private readonly ILogger _logger;
      private readonly CompositionHelper<IModuleContract, IModuleLocationAttribute> _menuCompositionHelper;
      private ICommand _startModuleCommand;

      public MainWindowViewModel()
      {
         // create logger
         _logger = new Logger.Logger();

         // load modules
         _logger.Info(this, "Load modules");
         _compositionHelper = new CompositionHelper<IModuleContract>();
         _compositionHelper.AssembleModuleComponents();

         // load menu modules
         _logger.Info(this, "Load menus");
         _menuCompositionHelper = new CompositionHelper<IModuleContract, IModuleLocationAttribute>();
         _compositionHelper.AssembleModuleComponents();
      }

      public IEnumerable<Lazy<IModuleContract>> Modules => _compositionHelper.Modules;

      public ICommand StartModuleCommand
      {
         get
         {
            return _startModuleCommand =
               _startModuleCommand ?? new RelayCommand<string>(x =>
               {
                  _logger.Info(this, $"Starting module {x}");
                  MessageBox.Show(x);
               });
         }
      }
   }
}