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

    [MenuGroupItem("About")]
    public class About : IModuleCommand
    {
        public string Id => GetType().Name;

        public bool Active
        {
            get { return true; }
            set { }
        }

        public ICommand Command => null;
        public string Name => Resources.Application.AboutCommandName;
        public IModuleContract Module { get; set; }
    }
}