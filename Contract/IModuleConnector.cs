namespace Hdd.Contract
{
   public interface IModuleConnector
   {
      /// <summary>
      ///    The module that runs the command.
      /// </summary>
      IModuleContract Module { get; set; }
   }
}