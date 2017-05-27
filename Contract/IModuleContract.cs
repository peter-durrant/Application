using System.Collections;

namespace Hdd.Contract
{
   public interface IModuleContract : IModuleConnector
   {
      string Name { get; }
      string Version { get; }
      string SayHello { get; }
   }
}