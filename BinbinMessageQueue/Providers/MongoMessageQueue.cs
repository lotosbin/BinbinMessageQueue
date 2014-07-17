using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using MongoRepository;
using ServiceStack;
using ServiceStack.Text;

namespace BinbinMessageQueue.Providers
{
    public class MongoMessageQueue : IMessageBus
    {
        public void PublishMessage(string channel, string message)
        {
            var repository = CreateRepository();
            repository.Add(new MongoMessage() { Channel = channel, TypeId = "", Message = message });
        }

        private static MongoRepository<MongoMessage> CreateRepository()
        {
            var repository = new MongoRepository<MongoMessage>("mongodb://localhost/messagebus");
            return repository;
        }

        public void Subscription(string[] channels, Action<string, string> onMessage)
        {
            var repository = CreateRepository();
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
            var attributes = message.GetType().GetCustomAttributes(typeof(GuidAttribute), false);
            if (attributes.Count() != 1)
            {
                throw new Exception("Cannot found Guid Attriburte");
            }
            var envelope = new MongoMessage()
            {
                Channel = channel,
                TypeId = message.GetType().GUID.ToString("N"),
                Message = message.SerializeToString(),
            };
            var repository = CreateRepository();
            repository.Add(envelope);
        }

        public void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage)
        {
            Subscription(channels, (channel, message) => onMessage(channel, DeserializeFromString<TModel>(message)));
        }

        public TModel DeserializeFromString<TModel>(string message)
        {
            return JsonSerializer.DeserializeFromString<TModel>(message);
        }

        public void Subscription(string[] channels, Action<string, string, string> onMessage)
        {
            var repository = CreateRepository();
            while (true)
            {
                foreach (var channel in channels)
                {
                    var message = repository.Where(m => m.Channel == channel).OrderBy(m => m.Id).FirstOrDefault();
                    if (message != null)
                    {
                        onMessage(channel, message.TypeId, message.Message);
                        repository.Delete(message);
                    }
                }
            }
        }
    }
}