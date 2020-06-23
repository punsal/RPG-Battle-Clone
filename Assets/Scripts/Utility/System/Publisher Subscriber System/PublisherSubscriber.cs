using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utility.System.Publisher_Subscriber_System
{
    public static class PublisherSubscriber
    {
        private static Dictionary<Type, IList> subscriber;
        private static bool isInitialized;

        private static void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            subscriber = new Dictionary<Type, IList>();
        }

        public static void Publish<TMessageType>(TMessageType messageType)
        {
            Initialize();

            var type = typeof(TMessageType);

            if (!subscriber.ContainsKey(type)) return;
            var actionList = new List<Subscription<TMessageType>>(subscriber[type].Cast<Subscription<TMessageType>>());

            foreach (var action in actionList) action.PublisherAction(messageType);
        }

        public static Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Initialize();

            var type = typeof(TMessageType);
            var actionDetail = new Subscription<TMessageType>(action /*, this*/);

            if (!subscriber.TryGetValue(type, out var actionList))
            {
                actionList = new List<Subscription<TMessageType>> {actionDetail};
                subscriber.Add(type, actionList);
            }
            else
            {
                actionList.Add(actionDetail);
            }

            return actionDetail;
        }

        public static void Unsubscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Initialize();

            var type = typeof(TMessageType);
            if (subscriber.ContainsKey(type)) subscriber[type].Remove(subscription);
        }
    }
}