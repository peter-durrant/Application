﻿using System;

namespace Hdd.CqrsEventSourcing
{
   public class MissingParameterLessConstructorException : Exception
   {
      public MissingParameterLessConstructorException(Type type)
         : base($"{type.FullName} has no constructor without paramerters. This can be either public or private")
      {
      }
   }
}