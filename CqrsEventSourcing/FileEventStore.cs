using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace Hdd.CqrsEventSourcing
{
   public class FileEventStore : IEventStore
   {
      private readonly string _filePath;
      private readonly JavaScriptSerializer _javaScriptSerializer;
      private readonly IEventPublisher _publisher;

      public FileEventStore(string filePath, IEventPublisher publisher)
      {
         _filePath = filePath;
         _publisher = publisher;
         _javaScriptSerializer = new JavaScriptSerializer(new SimpleTypeResolver());
      }

      public void Save<T>(IEnumerable<IEvent> events)
      {
         using (var file = File.AppendText(_filePath))
         {
            foreach (var @event in events)
            {
               file.WriteLine(_javaScriptSerializer.Serialize(@event));
               _publisher.Publish(@event);
            }
         }
      }

      public IEnumerable<IEvent> Get<T>(Guid aggregateId, int fromVersion)
      {
         var events = new List<IEvent>();
         if (!File.Exists(_filePath))
         {
            return events;
         }

         using (var fileStream = File.OpenRead(_filePath))
         {
            using (var streamReader = new StreamReader(fileStream))
            {
               string json;
               while ((json = streamReader.ReadLine()) != null)
               {
                  var @event = _javaScriptSerializer.Deserialize<IEvent>(json);
                  if (@event.Id == aggregateId)
                  {
                     events.Add(@event);
                  }
               }
            }
         }
         return events.Where(x => x.Version > fromVersion);
      }
   }
}