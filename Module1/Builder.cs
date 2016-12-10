using System;
using Hdd.CqrsEventSourcing;
using Hdd.Module1.Domain.WriteModel.Commands;
using Hdd.Module1.Domain.WriteModel.Handlers;

namespace Hdd.Module1
{
   internal class Builder
   {
      public Builder()
      {
         // create infrastructure
         var eventPublisher = new InProcessBus();
         var eventStore = new FileEventStore(@"z:\eventsource.txt", eventPublisher);
         //var eventStore = new InMemoryEventStore(eventPublisher);
         var repository = new Repository(eventStore);
         var session = new Session(repository);
         var commandHandler = new ItemCommandHandlers(session);

         // create a new aggregate root
         var id = Guid.NewGuid();
         var createItemCommand = new CreateItemCommand(id, "Item 1");
         commandHandler.Handle(createItemCommand);

         // rename the aggregate root
         var renameItemCommand = new RenameItemCommand(id, "Item 5", 1);
         commandHandler.Handle(renameItemCommand);
      }
   }
}