using System.Runtime.Serialization;

namespace BinbinMessageQueueTest
{
    [DataContract]
    public class TestMessage
    {
        [DataMember]
        public string Message { get; set; }
    }
}