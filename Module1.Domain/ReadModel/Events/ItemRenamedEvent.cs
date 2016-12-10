using System;
using Hdd.CqrsEventSourcing;

namespace Hdd.Module1.Domain.ReadModel.Events
{
   public class ItemRenamedEvent : IEvent
   {
      public readonly string NewName;

      // TODO remove - only required for JSON deserialisation?
      public ItemRenamedEvent()
      {
      }

      public ItemRenamedEvent(Guid id, string newName)
      {
         Id = id;
         NewName = newName;
      }

      public Guid Id { get; set; }
      public int Version { get; set; }
      public DateTimeOffset TimeStamp { get; set; }
   }
}