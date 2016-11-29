using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Hdd.Logger;

namespace Hdd.ModuleLoader
{
   public class CompositionHelper<T>
   {
      private readonly ILogger _logger;

      public CompositionHelper()
      {
         _logger = new Logger.Logger();
      }

      [ImportMany]
      public IEnumerable<Lazy<T>> Modules { get; set; }

      public void AssembleModuleComponents()
      {
         try
         {
            var catalog = new DirectoryCatalog(@".", "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            foreach (var module in Modules)
            {
               var moduleName = Assembly.GetAssembly(module.Value.GetType()).GetName();
               _logger.Info(this, $"Loaded {moduleName.FullName} {moduleName.Version} ({typeof(T).Namespace}.{typeof(T).Name})");
            }
         }
         catch (Exception e)
         {
            _logger.Error(this, "Error loading modules", e);
            throw;
         }
      }
   }
}