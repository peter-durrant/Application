using System;

namespace Hdd.CqrsEventSourcing
{
   public interface ICommand : IMessage
   {
      Guid Id { get; }
      int ExpectedVersion { get; set; }
   }
}