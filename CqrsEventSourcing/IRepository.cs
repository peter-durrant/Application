using System;

namespace Hdd.CqrsEventSourcing
{
   public interface IRepository
   {
      void Save<T>(AggregateRoot aggregate, int? expectedVersion = null)
         where T : AggregateRoot;

      T Get<T>(Guid aggregateId)
         where T : AggregateRoot;
   }
}