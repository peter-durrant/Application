using System;
using Hdd.CqrsEventSourcing;

namespace Hdd.Module1.Domain.WriteModel.Commands
{
   public class RenameItemCommand : ICommand
   {
      public readonly string NewName;

      public RenameItemCommand(Guid id, string newName, int originalVersion)
      {
         Id = id;
         NewName = newName;
         ExpectedVersion = originalVersion;
      }

      public Guid Id { get; }
      public int ExpectedVersion { get; set; }
   }
}