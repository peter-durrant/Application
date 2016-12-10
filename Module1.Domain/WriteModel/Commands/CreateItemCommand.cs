using System;
using Hdd.CqrsEventSourcing;

namespace Hdd.Module1.Domain.WriteModel.Commands
{
   public class CreateItemCommand : ICommand
   {
      public readonly string Name;

      public CreateItemCommand(Guid id, string name)
      {
         Id = id;
         Name = name;
      }

      public Guid Id { get; }
      public int ExpectedVersion { get; set; }
   }
}