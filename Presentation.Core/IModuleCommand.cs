using System;
using System.Windows.Input;
using Hdd.Contract;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public interface IModuleCommand : IMenuCommand, IModuleConnector
    {
    }

    public interface IModuleCommandWithEvents : IMenuCommandEvents, IModuleCommand
    {
    }
}