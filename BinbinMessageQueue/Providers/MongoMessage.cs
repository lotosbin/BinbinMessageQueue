using System.Runtime.Serialization;
using MongoRepository;

namespace BinbinMessageQueue.Providers
{
    [DataContract]
    internal class MongoMessage : Entity
    {
        [DataMember]
        public string Channel { get; set; }
        [DataMember]
        public string TypeId { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}