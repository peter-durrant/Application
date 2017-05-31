using System;
using System.ComponentModel.Composition;

namespace Hdd.Presentation.Core
{
    /// <summary>
    ///     Metadata attribute to specify the location of the module command (could be used to construct a menu, ribbon, etc.)
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModuleLocation : ExportAttribute, IModuleLocationAttribute
    {
        public ModuleLocation(string groupName, int groupOrder)
        {
            GroupName = groupName;
            GroupOrder = groupOrder;
        }

        public ModuleLocation(string groupName, int subGroup, int subGroupOrder)
        {
            GroupName = groupName;
            SubGroup = subGroup;
            SubGroupOrder = subGroupOrder;
        }

        public string GroupName { get; }
        public int GroupOrder { get; }
        public int SubGroup { get; }
        public int SubGroupOrder { get; }
    }
}