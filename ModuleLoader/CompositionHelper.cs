using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Hdd.Logger;
using Hdd.ModuleContract;

namespace Hdd.ModuleLoader
{
   public class CompositionHelper
   {
      private readonly ILogger _logger;

      public CompositionHelper()
      {
         _logger = new Logger.Logger();
      }

      [ImportMany]
      public IEnumerable<Lazy<IModuleContract>> ModulePlugins { get; set; }

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