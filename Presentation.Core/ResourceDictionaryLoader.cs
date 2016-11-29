using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace Hdd.Presentation.Core
{
   public static class ResourceDictionaryLoader
   {
      public static string GetLanguage
      {
         get
         {
            var language = "en-US";
            if (CultureInfo.DefaultThreadCurrentCulture != null)
            {
               language = CultureInfo.DefaultThreadCurrentCulture.Name;
            }
            return language;
         }
      }

      public static ResourceDictionary Load()
      {
         var resourceDictionary = new ResourceDictionary
         {
            Source =
               new Uri(
                  $"/{Assembly.GetCallingAssembly().GetName().Name}.Resources;component/resources/languages/{GetLanguage}/StringResources.xaml",
                  UriKind.RelativeOrAbsolute)
         };
         return resourceDictionary;
      }
   }
}