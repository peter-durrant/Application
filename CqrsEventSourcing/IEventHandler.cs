namespace Hdd.CqrsEventSourcing
{
   public interface IEventHandler<in T> : IHandler<T> where T : IEvent
   {
   }
}