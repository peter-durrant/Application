using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using Hdd.Logger;
using Hdd.ModuleContract;
using Hdd.Presentation.Core;

namespace Hdd.Module1
{
   [Export(typeof(IModuleContract))]
   public class Module1Module : IModuleContract
   {
      private readonly ILogger _logger;
      private readonly ResourceDictionary _resourceDictionary;

      public Module1Module()
      {
         _logger = new Logger.Logger();
         _resourceDictionary = ResourceDictionaryLoader.Load();
      }

      public string Name => "Module 1";

      public string Version => Assembly.GetAssembly(typeof(Module1Module)).GetName().Version.ToString();

      public void DoSomething()
      {
         _logger.Info(this, "DoSomething");
      }

      public string SayHello => (string) _resourceDictionary["Hello"];
   }
}