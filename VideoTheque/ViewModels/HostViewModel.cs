using System.Text.Json.Serialization;

namespace VideoTheque.ViewModels
{
    public class HostViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("nom")]
        public string Name { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}