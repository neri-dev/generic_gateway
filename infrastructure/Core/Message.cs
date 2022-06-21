namespace infrastructure
{
    public class Message
    {
        public string  Type { get; set; }
        public object Payload { get; set; }

        public Message(string type, object payload)
        {
            Type = type;
            Payload = payload;
        }
    }
}