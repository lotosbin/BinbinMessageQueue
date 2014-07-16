using System;
using BinbinMessageQueue.Providers;

namespace BinbinMessageQueueHandlers
{
    class Program
    {
        static void Main(string[] args)
        {
            new MongoMessageBus().Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            //MessageBus.Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            //new MongoMessageBus().Subscription<TestMessage>(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message.Message));
        }
    }
}
