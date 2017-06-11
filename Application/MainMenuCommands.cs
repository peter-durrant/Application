using System.Windows.Input;
using Hdd.Contract;
using Hdd.Presentation.Core;

namespace Hdd.Application
{
    [MenuGroupItem("File")]
    public class File : IModuleCommand
    {
        public string Id => GetType().Name;

        public bool Active
        {
            get { return true; }
            set { }
        }

        public ICommand Command => null;
        public string Name => Resources.Application.FileCommandName;
        public IModuleContract Module { get; set; }
    }

    [MenuGroupItem("Help")]
    public class Help : IModuleCommand
    {
        public string Id => GetType().Name;

        public bool Active
        {
            get { return true; }
            set { }
        }

        public ICommand Command => null;
        public string Name => Resources.Application.HelpCommandName;
        public IModuleContract Module { get; set; }
    }
}