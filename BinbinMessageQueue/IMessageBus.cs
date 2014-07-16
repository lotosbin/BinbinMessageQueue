using System;

namespace BinbinMessageQueue
{
    public interface IMessageBus
    {
        void PublishMessage(string channel, string message);
        void Subscription(string[] channels, Action<string, string> onMessage);
        void PublishMessage<TModel>(string channel, TModel message);
        void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage);
    }
}