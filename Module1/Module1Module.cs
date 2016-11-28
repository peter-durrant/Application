using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Hdd.Logger;
using Hdd.ModuleContract;

namespace Hdd.Module1
{
   [Export(typeof(IModuleContract))]
   public class Module1Module : IModuleContract
   {
      private readonly ILogger _logger;
      private ResourceDictionary _resourceDictionary;

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
                  new Uri($"/Module1;component/resources/languages/{language}/StringResources.xaml",
                     UriKind.RelativeOrAbsolute)
            };
            return (string) _resourceDictionary["Hello"];
         }
      }
   }
}