using Hdd.Contract;
using Menu.Core;

namespace Hdd.Presentation.Core
{
    public interface IModuleCommand : IMenuCommand, IModuleConnector
    {
    }
}