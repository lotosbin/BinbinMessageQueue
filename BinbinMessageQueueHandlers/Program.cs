using System;
using System.Runtime.Serialization;
using BinbinMessageQueue;
using BinbinMessageQueue.Providers;
using BinbinMessageQueueTest;

namespace BinbinMessageQueueHandlers
{
    class Program
    {

        static void Main(string[] args)
        {
            //new MongoMessageQueue().Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            IMessageBus queue = new MongoMessageQueue();
            //queue.Subscription<TestMessage>(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message.Message));
            queue.Subscription(new[] { "test" }, (channel, typeId, message) =>
            {
                if (!string.IsNullOrEmpty(typeId))
                {
                    if (new Guid(typeId) == new Guid("C2C37264-5CDD-4B1B-871D-AEE611673920"))
                    {
                        //testmessage1
                        var testMessage = queue.DeserializeFromString<TestMessage>(message);
                        Console.WriteLine("handle message:" + testMessage.Message);
                    }
                    else if (new Guid(typeId) == new Guid("E92214C4-A2BB-4201-B0D1-FB1C3E8F160B"))
                    {
                        //testmessage2
                        var testMessage = queue.DeserializeFromString<TestMessage2>(message);
                        Console.WriteLine("handle message:" + testMessage.Name);

                    }
                    else
                    {
                        throw new Exception("can not handle");
                    }
                }
            });
            //MessageBus.Subscription(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message));
            //new MongoMessageBus().Subscription<TestMessage>(new[] { "test" }, (channel, message) => Console.WriteLine("handle message:" + message.Message));
        }
    }
}
