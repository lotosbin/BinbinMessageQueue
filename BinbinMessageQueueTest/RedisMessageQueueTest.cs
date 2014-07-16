using BinbinMessageQueue.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinbinMessageQueueTest
{
    [TestClass]
    public class RedisMessageQueueTest
    {
       
        [TestMethod]
        public void TestMethod1()
        {
            new RedisMessageQueue().PublishMessage("test", "test message");
        }

    }
}
