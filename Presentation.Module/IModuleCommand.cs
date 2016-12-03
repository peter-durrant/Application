using System.Windows.Input;
using Hdd.Contract;

namespace Hdd.Presentation.Module
{
   public interface IModuleCommand
   {
      /// <summary>
      ///    The module that runs the command.
      /// </summary>
      IModuleContract Module { get; set; }

      /// <summary>
      ///    Is true if the command can be run.
      /// </summary>
      bool Active { get; }

      /// <summary>
      ///    Command to execute.
      /// </summary>
      /// <returns>The command action.</returns>
      ICommand Command { get; }

      /// <summary>
      ///    Display name of the command.
      /// </summary>
      /// <returns>The command action.</returns>
      string Name { get; }
   }
}