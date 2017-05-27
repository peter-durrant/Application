using System;
using System.ComponentModel.Composition;

namespace Hdd.Presentation.Module
{
    /// <summary>
    ///     Metadata attribute to specify the location of the module command (could be used to construct a menu, ribbon, etc.)
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModuleLocation : ExportAttribute, IModuleLocationAttribute
    {
        public ModuleLocation(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; }
    }
}