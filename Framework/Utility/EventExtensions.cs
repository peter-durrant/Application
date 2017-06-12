using System;

namespace HDD.Utility
{
    public static class EventExtensions
    {
        public static void Raise(this EventHandler handler, object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T e)
        {
            handler?.Invoke(sender, new EventArgs<T>(e));
        }
    }
}