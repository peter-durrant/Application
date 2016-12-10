using Hdd.CqrsEventSourcing;
using Hdd.Module1.Domain.ReadModel.Events;

namespace Hdd.Module1.Domain.ReadModel.Handlers
{
   public class ItemEventHandler : IEventHandler<ItemCreatedEvent>, IEventHandler<ItemRenamedEvent>
   {
      public void Handle(ItemCreatedEvent message)
      {
         // TODO create a new record
      }

      public void Handle(ItemRenamedEvent message)
      {
         // TODO find an existing record and update
      }
   }
}