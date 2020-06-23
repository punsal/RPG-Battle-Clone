using System;

namespace Utility.System.Publisher_Subscriber_System
{
    public class Subscription<TMessage> : IDisposable
    {
        //private readonly PublisherSubscriber publisherSubscriber;
        private bool isDisposed;

        public Subscription(Action<TMessage> publisherAction /*, PublisherSubscriber publisherSubscriber*/)
        {
            PublisherAction = publisherAction;
            //this.publisherSubscriber = publisherSubscriber;
        }

        public Action<TMessage> PublisherAction { get; }

        public void Dispose()
        {
            //publisherSubscriber.Unsubscribe(this);
            PublisherSubscriber.Unsubscribe(this);
            isDisposed = true;
        }

        ~Subscription()
        {
            if (!isDisposed) Dispose();
        }
    }
}