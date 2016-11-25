using System.ComponentModel.Composition;
using System.Reflection;
using Hdd.Logger;
using Hdd.ModuleContract;

namespace Hdd.Module1
{
   [Export(typeof(IModuleContract))]
   public class Module1Module : IModuleContract
   {
      private readonly ILogger _logger;

      public Module1Module()
      {
         _logger = new Logger.Logger();
      }

      public string Name => "Module 1";

      public string Version => Assembly.GetAssembly(typeof(Module1Module)).GetName().Version.ToString();

      public void DoSomething()
      {
         _logger.Info(this, "DoSomething");
      }
   }
}