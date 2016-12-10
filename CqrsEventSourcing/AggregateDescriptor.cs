namespace Hdd.CqrsEventSourcing
{
   internal class AggregateDescriptor
   {
      public AggregateRoot Aggregate { get; set; }
      public int Version { get; set; }
   }
}