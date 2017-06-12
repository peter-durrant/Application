using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;

namespace Hdd.Module2.Presentation
{
    [MenuGroupItem("File", "Print|5")]
    [Export(typeof(IModuleCommand))]
    public class Print : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public Print()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Print(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged();
            }
        }

        public string Name => Resources.Module2.PrintCommandName;
    }

    [MenuGroupItem("File", "Exit|9999|Exit")]
    [Export(typeof(IModuleCommand))]
    public class Exit : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public Exit()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule2Contract).Exit(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged();
            }
        }

        public string Name => Resources.Module2.ExitCommandName;
    }
}