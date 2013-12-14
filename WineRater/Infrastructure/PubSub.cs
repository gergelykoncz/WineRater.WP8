using System;
using System.Collections.Generic;
using System.Linq;

namespace WineRater.Infrastructure
{
    public static class PubSub<T>
    {
        private static List<Notification<T>> notifications = new List<Notification<T>>();

        public static void RegisterForEvent(string name, Action<T> callback)
        {
            var notification = notifications.SingleOrDefault(x => x.Name == name);
            if (notification == null)
            {
                notification = new Notification<T>();
                notification.Name = name;
                notification.Subscribers.Add(callback);
                notifications.Add(notification);
            }
            else if(notification.Subscribers.Contains(callback) == false)
            {
                notification.Subscribers.Add(callback);
            }
        }

        public static void RaiseEvent(string name, T argument)
        {
            var notification = notifications.SingleOrDefault(x => x.Name == name);
            if (notification != null)
            {
                foreach (var subscriber in notification.Subscribers)
                {
                    subscriber.Invoke(argument);
                }
            }
        }
    }
}
