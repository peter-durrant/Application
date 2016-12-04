using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Hdd.Contract;
using Hdd.Logger;

namespace Hdd.ModuleLoader
{

   #region CompositionHelper<T>

   public class CompositionHelper<T> where T : IModuleConnector
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
               _logger.Info(this,
                  $"{module.Value} - Loaded {moduleName.FullName} {moduleName.Version} ({typeof(T).Namespace}.{typeof(T).Name})");
               module.Value.Module = (IModuleContract) module.Value;
            }
         }
         catch (Exception e)
         {
            _logger.Error(this, "Error loading modules", e);
            throw;
         }
      }
   }

   #endregion CompositionHelper<T1>

   #region CompositionHelper<T1, T2>

   public class CompositionHelper<T1, T2> where T1 : IModuleConnector
   {
      private readonly ILogger _logger;

      public CompositionHelper()
      {
         _logger = new Logger.Logger();
      }

      [ImportMany]
      private IEnumerable<Lazy<T1, T2>> ImportedModules { get; set; }

      public IEnumerable<Lazy<T1, T2>> Modules { get; set; }

      public void AssembleModuleComponents(IEnumerable<Lazy<IModuleContract>> modules)
      {
         try
         {
            var moduleList = new List<Lazy<T1, T2>>();
            foreach (var module in modules)
            {
               var moduleName = module.Value.GetType().Assembly.GetName().Name;
               var catalog = new DirectoryCatalog(@".", $"{moduleName}*.dll");
               var container = new CompositionContainer(catalog);
               container.ComposeParts(this);

               foreach (var importedModule in ImportedModules)
               {
                  var importedModuleName = Assembly.GetAssembly(importedModule.Value.GetType()).GetName();
                  _logger.Info(this,
                     $"{importedModule.Value} - Loaded {importedModuleName.FullName} {importedModuleName.Version} ({typeof(T1).Namespace}.{typeof(T1).Name} and {typeof(T2).Namespace}.{typeof(T2).Name})");
                  // for each module found, set it's Module reference to the core module
                  importedModule.Value.Module = module.Value.Module;
               }
               moduleList.AddRange(ImportedModules);
            }
            Modules = moduleList;
         }
         catch (Exception e)
         {
            _logger.Error(this, "Error loading modules", e);
            throw;
         }
      }
   }

   #endregion CompositionHelper<T1, T2>
}