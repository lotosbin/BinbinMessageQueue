using System;
using System.Runtime.Serialization;
using BinbinMessageQueue.Providers;
using BinbinMessageQueueTest;

namespace BinbinMessageQueueHandlers
{
    class Program
    {

        static void Main(string[] args)
        {
            //new MongoMessageQueue().Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            new MongoMessageQueue().Subscription<TestMessage>(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message.Message));
            //MessageBus.Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            //new MongoMessageBus().Subscription<TestMessage>(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message.Message));
        }
    }
}
