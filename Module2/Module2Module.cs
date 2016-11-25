using System.ComponentModel.Composition;
using System.Reflection;
using Hdd.Logger;
using Hdd.ModuleContract;

namespace Hdd.Module2
{
   [Export(typeof(IModuleContract))]
   public class Module2Module : IModuleContract
   {
      private readonly ILogger _logger;

      public Module2Module()
      {
         _logger = new Logger.Logger();
      }

      public string Name => "Module 2";

      public string Version => Assembly.GetAssembly(typeof(Module2Module)).GetName().Version.ToString();

      public void DoSomething()
      {
         _logger.Info(this, "DoSomething");
      }
   }
}