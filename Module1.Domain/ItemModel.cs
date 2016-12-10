using System;
using Hdd.CqrsEventSourcing;
using Hdd.Module1.Domain.ReadModel.Events;

namespace Hdd.Module1.Domain
{
   public class ItemModel : AggregateRoot
   {
      public ItemModel()
      {
      }

      public ItemModel(Guid id, string name)
      {
         Id = id;
         ApplyChange(new ItemCreatedEvent(id, name));
      }

      public void ChangeName(string newName)
      {
         if (string.IsNullOrEmpty(newName))
         {
            throw new ArgumentException("newName");
         }
         ApplyChange(new ItemRenamedEvent(Id, newName));
      }
   }
}