using System.Text.Json.Serialization;

namespace VideoTheque.ViewModels
{
    public class EmpruntViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("duration")]
        public long Duration { get; set; }
        
        [JsonPropertyName("ageRating")]
        public AgeRatingViewModel AgeRating { get; set; }
        
        [JsonPropertyName("genre")]
        public GenreViewModel Genre { get; set; }
        
        [JsonPropertyName("director")]
        public PersonneViewModel Director { get; set; }
        
        [JsonPropertyName("scenarist")]
        public PersonneViewModel Scenarist { get; set; }
        
        [JsonPropertyName("firstActor")]
        public PersonneViewModel FirstActor { get; set; }
        
        [JsonPropertyName("support")]
        public string Support { get; set; }

        public override string ToString()
        {
            return "EmpruntViewModel{" +
                   "Title='" + Title + '\'' +
                   ", Duration=" + Duration +
                   ", AgeRating=" + AgeRating?.Name +
                   ", Genre=" + Genre?.Name +
                   ", Director=" + Director?.FullName +
                   ", Scenarist=" + Scenarist?.FullName +
                   ", FirstActor=" + FirstActor?.FullName +
                   ", Support='" + Support + '\'' +
                   '}';
        }
    }
}