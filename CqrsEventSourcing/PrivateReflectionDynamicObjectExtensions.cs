﻿namespace Hdd.CqrsEventSourcing
{
   internal static class PrivateReflectionDynamicObjectExtensions
   {
      public static dynamic AsDynamic(this object o)
      {
         return PrivateReflectionDynamicObject.WrapObjectIfNeeded(o);
      }
   }
}