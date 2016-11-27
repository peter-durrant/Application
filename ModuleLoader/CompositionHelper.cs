using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
         }
         catch (Exception e)
         {
            _logger.Error(this, "Error loading modules", e);
            throw;
         }
      }
   }
}