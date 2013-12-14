using System;
using System.Collections.Generic;

namespace WineRater.Infrastructure
{
    public class Notification<T>
    {
        public string Name { get; set; }
        public List<Action<T>> Subscribers { get; set; }

        public Notification()
        {
            Subscribers = new List<Action<T>>();
        }
    }
}
