using System.Runtime.Serialization;
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
            new MongoMessageQueue().PublishMessage("test", "test message");
        }

        [TestMethod]
        public void TestMongoMessageQueueStrongType()
        {
            new MongoMessageQueue().PublishMessage<TestMessage>("test", new TestMessage()
            {
                Message = "test user"
            });
        }
    }
}