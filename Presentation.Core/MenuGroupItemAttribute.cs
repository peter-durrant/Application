using System;
using System.ComponentModel.Composition;

namespace Hdd.Presentation.Core
{
    /// <summary>
    ///     Metadata attribute to specify the location of the module command (could be used to construct a menu, ribbon, etc.)
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MenuGroupItemAttribute : ExportAttribute, IMenuGroupItemAttribute
    {
        public MenuGroupItemAttribute(params string[] items)
        {
            Items = items;
        }

        public string[] Items { get; }
    }
}