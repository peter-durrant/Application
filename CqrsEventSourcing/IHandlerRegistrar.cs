using System;

namespace Hdd.CqrsEventSourcing
{
   public interface IHandlerRegistrar
   {
      void RegisterHandler<T>(Action<T> handler) where T : IMessage;
   }
}