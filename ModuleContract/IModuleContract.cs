namespace Hdd.ModuleContract
{
   public interface IModuleContract
   {
      string Name { get; }
      string Version { get; }
      void DoSomething();
   }
}