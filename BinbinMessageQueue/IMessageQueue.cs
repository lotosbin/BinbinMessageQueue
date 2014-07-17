using System;

namespace BinbinMessageQueue
{
    public interface IMessageQueue
    {
        #region New region

        void PublishMessage(string channel, string typeId, string message);

        /// <summary>
        /// Action channel,typeid,message 
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="onMessage"></param>
        void Subscription(string[] channels, Action<string, string, string> onMessage);

        #endregion

        #region New region

        void PublishMessage<TModel>(string channel, TModel model);
        string GetTypeId(Type modelType);
        string GetTypeId<TModel>();
        #endregion

        TModel DeserializeFromString<TModel>(string message);
        string SerialalizeToString<TModel>(TModel model);
    }
}