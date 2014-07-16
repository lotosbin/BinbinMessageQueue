using System;
using System.Linq;
using MongoRepository;
using ServiceStack;
using ServiceStack.Text;

namespace BinbinMessageQueue.Providers
{
    public class MongoMessageBus : IMessageBus
    {
        public void PublishMessage(string channel, string message)
        {
            var repository = new MongoRepository<MongoMessage>("mongodb://localhost/messagebus");
            repository.Add(new MongoMessage() { Channel = channel, Message = message });
        }

        public void Subscription(string[] channels, Action<string, string> onMessage)
        {
            var repository = new MongoRepository<MongoMessage>("mongodb://localhost/messagebus");
            while (true)
            {
                foreach (var channel in channels)
                {
                    var message = repository.Where(m => m.Channel == channel).OrderBy(m => m.Id).FirstOrDefault();
                    if (message != null)
                    {
                        onMessage(channel, message.Message);
                        repository.Delete(message);
                    }
                }
            }
        }

        public void PublishMessage<TModel>(string channel, TModel message)
        {
            PublishMessage(channel, message.SerializeToString());
        }

        public void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage)
        {
            Subscription(channels, (channel, message) => onMessage(channel, JsonSerializer.DeserializeFromString<TModel>(message)));
        }
    }
}
