﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using MongoRepository;
using ServiceStack;
using ServiceStack.Text;

namespace BinbinMessageQueue.Providers
{
    public class MongoMessageQueue : IMessageQueue
    {
        private static MongoRepository<MongoMessage> CreateRepository()
        {
            var repository = new MongoRepository<MongoMessage>("mongodb://localhost/messagebus");
            return repository;
        }

        #region New region

        public void PublishMessage<TModel>(string channel, TModel model)
        {

            var typeId = GetTypeId<TModel>();
            var message = SerialalizeToString(model);
            PublishMessage(channel, typeId, message);
        }

        public string SerialalizeToString<TModel>(TModel model)
        {
            return model.SerializeToString();
        }

        public void PublishMessage(string channel, string typeId, string message)
        {
            var envelope = new MongoMessage()
            {
                Channel = channel,
                TypeId = typeId,
                Message = message,
            };
            var repository = CreateRepository();
            repository.Add(envelope);
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

        #endregion

        public string GetTypeId(Type modelType)
        {
            var attributes = modelType.GetCustomAttributes(typeof(GuidAttribute), false);
            if (attributes.Count() != 1)
            {
                throw new Exception("Cannot found Guid Attriburte");
            }
            return modelType.GUID.ToString("N");
        }

        public string GetTypeId<TModel>()
        {
            return GetTypeId(typeof(TModel));
        }

        public TModel DeserializeFromString<TModel>(string message)
        {
            return JsonSerializer.DeserializeFromString<TModel>(message);
        }
    }
}