using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;
using Hdd.Presentation.Module;

namespace Hdd.Module2.Presentation
{
   [ModuleLocation("File")]
   [Export(typeof(IModuleCommand))]
   public class Print : IModuleCommand
   {
      private ICommand _command;

      public ICommand Command
      {
         get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Print(); }); }
      }

      public IModuleContract Module { get; set; }

      public bool Active => true;

      public string Name => (string) Module?.ResourceDictionary["PrintCommandName"];
   }

   [ModuleLocation("File")]
   [Export(typeof(IModuleCommand))]
   public class Exit : IModuleCommand
   {
      private ICommand _command;

      public ICommand Command
      {
         get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Exit(); }); }
      }

      public IModuleContract Module { get; set; }

      public bool Active => true;

      public string Name => (string) Module?.ResourceDictionary["ExitCommandName"];
   }
}