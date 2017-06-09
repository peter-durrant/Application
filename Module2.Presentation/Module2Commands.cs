using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;

namespace Hdd.Module2.Presentation
{
    [MenuGroupItem("File", "Print|5")]
    [Export(typeof(IModuleCommand))]
    public class Print : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Print(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => true;

        public string Name => Resources.Module2.PrintCommandName;
    }

    [MenuGroupItem("File", "Exit|9999")]
    [Export(typeof(IModuleCommand))]
    public class Exit : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Exit(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => true;

        public string Name => Resources.Module2.ExitCommandName;
    }
}