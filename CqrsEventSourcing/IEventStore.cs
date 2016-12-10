﻿using System;
using System.Collections.Generic;

namespace Hdd.CqrsEventSourcing
{
   public interface IEventStore
   {
      void Save<T>(IEnumerable<IEvent> events);

      IEnumerable<IEvent> Get<T>(Guid aggregateId, int fromVersion);
   }
}