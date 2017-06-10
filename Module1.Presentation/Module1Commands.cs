using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;

namespace Hdd.Module1.Presentation
{
    [MenuGroupItem("File", "Open|1")]
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

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "OpenSub", "Some")]
    [Export(typeof(IModuleCommand))]
    public class OpenSome : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => false;

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "OpenSub", "All")]
    [Export(typeof(IModuleCommand))]
    public class OpenAll : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => true;

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "Close|10")]
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

        public bool Active => false;

        public string Name => Resources.Module1.CloseCommandName;
    }

    [MenuGroupItem("Help", "About")]
    [Export(typeof(IModuleCommand))]
    public class About : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).About(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => true;

        public string Name => Resources.Module1.CloseCommandName;
    }

    [MenuGroupItem("Help", "SendFeedback|Feedback")]
    [Export(typeof(IModuleCommand))]
    public class SendFeedback : IModuleCommand
    {
        private ICommand _command;

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).SendFeedback(); }); }
        }

        public IModuleContract Module { get; set; }

        public string Id => GetType().Name;

        public bool Active => true;

        public string Name => Resources.Module1.CloseCommandName;
    }
}