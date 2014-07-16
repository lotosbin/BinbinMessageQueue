using System;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace BinbinMessageQueue.Providers
{
    public class RedisMessageQueue : IMessageBus
    {
        #region Core

        public void PublishMessage(string channel, string message)
        {
            using (var client = new RedisClient("localhost", 6379))
            {
                client.PublishMessage(channel, message);
            }
        }

        public void Subscription(string[] channels, Action<string, string> onMessage)
        {
            var client = new RedisClient("localhost", 6379);

            using (var subscription = client.CreateSubscription())
            {
                subscription.OnMessage += onMessage;
                subscription.SubscribeToChannels(channels);
            }

        }

        #endregion
        #region strong type
        public void PublishMessage<TModel>(string channel, TModel message)
        {
            PublishMessage(channel, message.SerializeToString());
        }

        public void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage)
        {
            Subscription(channels, (channel, message) => onMessage(channel, JsonSerializer.DeserializeFromString<TModel>(message)));
        }
        #endregion
    }
}