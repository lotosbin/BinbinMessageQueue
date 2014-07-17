using System;
using System.Runtime.Serialization;
using BinbinMessageQueue;
using BinbinMessageQueue.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinbinMessageQueueTest
{
    [TestClass]
    public class MongoMessageQueueTest
    {

        [TestMethod]
        public void TestMongoMessageQueue()
        {
            IMessageQueue queue = new MongoMessageQueue();
            queue.PublishMessage("test", "", "test message");
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void TestMongoMessageQueueStrongType_NoGuidAttribute_ShouldException()
        {
            new MongoMessageQueue().PublishMessage("test", new TestMessageNoGuidAttribute()
            {
                Message = "test user"
            });
        }
        [TestMethod]
        public void TestMongoMessageQueueStrongType()
        {
            new MongoMessageQueue().PublishMessage("test", new TestMessage()
            {
                Message = "test user 1 "
            });
            new MongoMessageQueue().PublishMessage("test", new TestMessage2()
            {
                Name = "test user 2"
            });
        }
    }
}