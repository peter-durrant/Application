using Hdd.CqrsEventSourcing;
using Hdd.Module1.Domain.WriteModel.Commands;

namespace Hdd.Module1.Domain.WriteModel.Handlers
{
   public class ItemCommandHandlers : ICommandHandler<CreateItemCommand>, ICommandHandler<RenameItemCommand>
   {
      private readonly ISession _session;

      public ItemCommandHandlers(ISession session)
      {
         _session = session;
      }

      public void Handle(CreateItemCommand command)
      {
         var item = new ItemModel(command.Id, command.Name);
         _session.Add(item);
         _session.Commit<ItemModel>();
      }

      public void Handle(RenameItemCommand command)
      {
         var item = _session.Get<ItemModel>(command.Id, command.ExpectedVersion);
         item.ChangeName(command.NewName);
         _session.Add(item);
         _session.Commit<ItemModel>();
      }
   }
}