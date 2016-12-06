using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;
using Hdd.Presentation.Module;

namespace Hdd.Module1.Presentation
{
   [ModuleLocation("File")]
   [Export(typeof(IModuleCommand))]
   public class Open : IModuleCommand
   {
      private ICommand _command;

      public ICommand Command
      {
         get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
      }

      public IModuleContract Module { get; set; }

      public string Id => GetType().Name;

      public bool Active => true;

      public string Name => (string) Module?.ResourceDictionary["OpenCommandName"];
   }

   [ModuleLocation("File")]
   [Export(typeof(IModuleCommand))]
   public class Close : IModuleCommand
   {
      private ICommand _command;

      public ICommand Command
      {
         get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Close(); }); }
      }

      public IModuleContract Module { get; set; }

      public string Id => GetType().Name;

      public bool Active => true;

      public string Name => (string) Module?.ResourceDictionary["CloseCommandName"];
   }
}