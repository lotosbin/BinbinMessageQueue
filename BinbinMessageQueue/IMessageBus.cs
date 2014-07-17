using System;

namespace BinbinMessageQueue
{
    public interface IMessageBus
    {
        void PublishMessage(string channel, string message);
        void Subscription(string[] channels, Action<string, string> onMessage);
        void PublishMessage<TModel>(string channel, TModel message);
        [Obsolete]
        void Subscription<TModel>(string[] channels, Action<string, TModel> onMessage);
        /// <summary>
        /// Action channel,typeid,message 
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="onMessage"></param>
        void Subscription(string[] channels, Action<string, string, string> onMessage);

        TModel DeserializeFromString<TModel>(string message);
    }
}