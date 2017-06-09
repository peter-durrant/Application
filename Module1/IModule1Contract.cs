using Hdd.Contract;

namespace Hdd.Module1
{
   public interface IModule1Contract : IModuleContract
   {
      void Open();
      void Close();
      void About();
       void SendFeedback();
   }
}