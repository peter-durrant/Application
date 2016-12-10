﻿namespace Hdd.CqrsEventSourcing
{
   public interface ICommandSender
   {
      void Send<T>(T command) where T : ICommand;
   }
}