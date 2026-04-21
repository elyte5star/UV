
namespace WebAPI.Domain.Entities

{
    public class MQTTData 
    {
        public string Topic { get; set; }
        public string Payload { get; set; }
        public DateTime MessageDate { get; set; }

        public string ClientId { get; set; }
    }
}