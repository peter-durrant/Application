using System;
using System.Runtime.Remoting.Messaging;

namespace Hdd.CqrsEventSourcing
{
   public interface IEvent : IMessage
   {
      Guid Id { get; set; }
      int Version { get; set; }
      DateTimeOffset TimeStamp { get; set; }
   }
}