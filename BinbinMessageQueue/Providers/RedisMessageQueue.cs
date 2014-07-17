using System;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace BinbinMessageQueue.Providers
{
    public class RedisMessageQueue : IMessageQueue
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
        public void PublishMessage<TModel>(string channel, TModel model)
        {
            PublishMessage(channel, model.SerializeToString());
        }

        public string GetTypeId(Type modelType)
        {
            throw new NotImplementedException();
        }

        public string GetTypeId<TModel>()
        {
            throw new NotImplementedException();
        }

        public void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage)
        {
            Subscription(channels, (channel, message) => onMessage(channel, JsonSerializer.DeserializeFromString<TModel>(message)));
        }

        public void PublishMessage(string channel, string typeId, string message)
        {
            throw new NotImplementedException();
        }

        public void Subscription(string[] channels, Action<string, string, string> onMessage)
        {
            throw new NotImplementedException();
        }

        public TModel DeserializeFromString<TModel>(string message)
        {
            throw new NotImplementedException();
        }

        public string SerialalizeToString<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}