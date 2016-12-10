using System;
using Hdd.CqrsEventSourcing;

namespace Hdd.Module1.Domain.ReadModel.Events
{
   public class ItemCreatedEvent : IEvent
   {
      public readonly string Name;

      // TODO remove - only required for JSON deserialisation?
      public ItemCreatedEvent()
      {
      }

      public ItemCreatedEvent(Guid id, string name)
      {
         Id = id;
         Name = name;
      }

      public Guid Id { get; set; }
      public int Version { get; set; }
      public DateTimeOffset TimeStamp { get; set; }
   }
}