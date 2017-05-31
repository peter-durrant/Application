using Hdd.Contract;

namespace Hdd.Presentation.Core
{
    public class ModuleLocationName
    {
        private const string ModuleLocationPrefix = "ModuleLocation";
        private readonly IModuleContract _moduleContract;

        public ModuleLocationName(IModuleContract moduleContract)
        {
            _moduleContract = moduleContract;
        }

        public string ModuleLocation(string moduleLocation)
        {
            return moduleLocation;
        }
    }
}