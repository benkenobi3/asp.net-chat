using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;


namespace asp.net_chat.DataModel
{
    
    public class Message
    {

        public Message() {}
        public Message(string json)
        {
            JObject jObject = JObject.Parse(json);
            Timestamp = (long) jObject["timestamp"];
            Username = (string) jObject["username"];
            Text = (string) jObject["message"];
        }
        
        [Key]
        public int  Id { get; set; }
        public long Timestamp { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        
        public override string ToString()
        {
            return Id.ToString();
        }
        
    }
}