using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace BinbinMessageQueueTest
{
    [DataContract]
    public class TestMessageNoGuidAttribute
    {
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    [Guid("C2C37264-5CDD-4B1B-871D-AEE611673920")]
    public class TestMessage
    {
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    [Guid("E92214C4-A2BB-4201-B0D1-FB1C3E8F160B")]
    public class TestMessage2
    {
        [DataMember]
        public string Name { get; set; }
    }
}