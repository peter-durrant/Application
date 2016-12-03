using System.Collections;

namespace Hdd.Contract
{
   public interface IModuleContract
   {
      IDictionary ResourceDictionary { get; }
      string Name { get; }
      string Version { get; }
      string SayHello { get; }
   }
}