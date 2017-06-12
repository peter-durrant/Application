using System.ComponentModel.Composition;
using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;

namespace Hdd.Module1.Presentation
{
    [MenuGroupItem("File", "Open|1")]
    [Export(typeof(IModuleCommand))]
    public class Open : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public Open()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
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

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "OpenSub")]
    [Export(typeof(IModuleCommand))]
    public class OpenSub : NotifyPropertyChanged, IModuleCommand
    {
        public string Id => GetType().Name;

        public bool Active
        {
            get { return true; }
            set { }
        }

        public ICommand Command { get; }
        public string Name => Resources.Module1.OpenSubCommandName;
        public IModuleContract Module { get; set; }
    }

    [MenuGroupItem("File", "OpenSub", "Some")]
    [Export(typeof(IModuleCommand))]
    public class OpenSome : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public OpenSome()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
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

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "OpenSub", "All")]
    [Export(typeof(IModuleCommand))]
    public class OpenAll : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public OpenAll()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Open(); }); }
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

        public string Name => Resources.Module1.OpenCommandName;
    }

    [MenuGroupItem("File", "Close|10")]
    [Export(typeof(IModuleCommand))]
    public class Close : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public Close()
        {
            Active = false;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).Close(); }); }
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

        public string Name => Resources.Module1.CloseCommandName;
    }

    [MenuGroupItem("Help", "About")]
    [Export(typeof(IModuleCommand))]
    public class About : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public About()
        {
            Active = true;
        }

        public ICommand Command
        {
            get
            {
                return _command = _command ?? new RelayCommand(x =>
                {
                    (Module as IModule1Contract).About();
                    Active = false;
                });
            }
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

        public string Name => Resources.Module1.AboutCommandName;
    }

    [MenuGroupItem("Help", "SendFeedback|Feedback")]
    [Export(typeof(IModuleCommand))]
    public class SendFeedback : NotifyPropertyChanged, IModuleCommand
    {
        private bool _active;
        private ICommand _command;

        public SendFeedback()
        {
            Active = true;
        }

        public ICommand Command
        {
            get { return _command = _command ?? new RelayCommand(x => { (Module as IModule1Contract).SendFeedback(); }); }
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

        public string Name => Resources.Module1.SendFeedbackCommandName;
    }
}