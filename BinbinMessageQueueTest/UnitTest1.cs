using BinbinMessageQueue.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinbinMessageQueueTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //MessageBus.PublishMessage("test", "test message");
            //RedisMessageBus.PublishMessage("test", new TestMessage() { Message = "test message2" });
        }
        [TestMethod]
        public void TestMongoMessageBus()
        {
            new MongoMessageBus().PublishMessage("test", "test message");
            new MongoMessageBus().PublishMessage("test", "test message1");
            new MongoMessageBus().PublishMessage("test1", "test message2");
            new MongoMessageBus().PublishMessage("test", "test message3");
            new MongoMessageBus().PublishMessage("test1", "test message4");
            //RedisMessageBus.PublishMessage("test", new TestMessage() { Message = "test message2" });
        }
    }
}
