using System.Collections;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using Hdd.Contract;
using Hdd.Logger;
using Hdd.Presentation.Core;

namespace Hdd.Module1
{
   [Export(typeof(IModuleContract))]
   public class Module1Module : IModule1Contract
   {
      private readonly ILogger _logger;
      private readonly ResourceDictionary _resourceDictionary;

      public Module1Module()
      {
         _logger = new Logger.Logger();
         _resourceDictionary = ResourceDictionaryLoader.Load();
      }

      public IModuleContract Module { get; set; }

      public void Open()
      {
         _logger.Info(this, "Open");
      }

      public void Close()
      {
         _logger.Info(this, "Close");
      }

      public IDictionary ResourceDictionary => _resourceDictionary;

      public string Name => Assembly.GetAssembly(GetType()).GetName().Name;

      public string Version => Assembly.GetAssembly(typeof(Module1Module)).GetName().Version.ToString();

      public string SayHello => (string) _resourceDictionary["Hello"];
   }
}