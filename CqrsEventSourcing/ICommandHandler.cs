namespace Hdd.CqrsEventSourcing
{
   public interface ICommandHandler
   {
   }

   public interface ICommandHandler<in T> : ICommandHandler
      where T : ICommand
   {
      void Handle(T command);
   }
}