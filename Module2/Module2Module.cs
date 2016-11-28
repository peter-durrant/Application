using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Hdd.Logger;
using Hdd.ModuleContract;

namespace Hdd.Module2
{
   [Export(typeof(IModuleContract))]
   public class Module2Module : IModuleContract
   {
      private readonly ILogger _logger;
      private ResourceDictionary _resourceDictionary;

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

      public string SayHello
      {
         get
         {
            var language = "en-US";
            if (CultureInfo.DefaultThreadCurrentCulture != null)
            {
               language = CultureInfo.DefaultThreadCurrentCulture.Name;
            }
            _resourceDictionary = new ResourceDictionary
            {
               Source =
                  new Uri($"/Module2;component/resources/languages/{language}/StringResources.xaml",
                     UriKind.RelativeOrAbsolute)
            };
            return (string) _resourceDictionary["Hello"];
         }
      }
   }
}