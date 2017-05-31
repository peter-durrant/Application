using System;
using System.Collections.Generic;
using System.Windows.Input;
using Hdd.Contract;

namespace Hdd.Presentation.Core
{
    public class MenuGroups
    {
        public Dictionary<Tuple<int, string>, MenuItem> Groups { get; set; }
    }

    public class MenuItem : IModuleCommand
    {
        public MenuItem()
        {
        }

        public MenuItem(IModuleCommand moduleCommand)
        {
            Module = moduleCommand.Module;
            Id = moduleCommand.Id;
            Active = moduleCommand.Active;
            Command = moduleCommand.Command;
            Name = moduleCommand.Name;
        }

        public HashSet<MenuItem> ChildItems { get; set; }
        public IModuleContract Module { get; set; }
        public string Id { get; }
        public bool Active { get; }
        public ICommand Command { get; }
        public string Name { get; set; }
    }
}