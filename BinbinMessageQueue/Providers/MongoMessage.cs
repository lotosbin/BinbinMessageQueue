using System.Runtime.Serialization;
using MongoRepository;

namespace BinbinMessageQueue.Providers
{
    [DataContract]
    internal class MongoMessage : Entity
    {
        public string Channel { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}