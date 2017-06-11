using System;

namespace Menu.Core
{
    public interface IMenuCommandEvents
    {
        /// <summary>
        ///     Active changed
        /// </summary>
        event EventHandler ActiveChanged;
    }
}