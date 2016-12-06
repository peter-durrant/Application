using Hdd.Contract;

namespace Hdd.Presentation.Module
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
         return (string)_moduleContract.ResourceDictionary[string.Concat(ModuleLocationPrefix, moduleLocation)];
      }
   }
}