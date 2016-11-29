namespace Hdd.ModuleContract
{
   public interface IModuleContract
   {
      string Name { get; }
      string Version { get; }
      string SayHello { get; }
   }
}