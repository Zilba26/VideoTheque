using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class FilmViewModel
    {
        [JsonPropertyName("id")]
        private int Id { get; set; }
        
        [JsonPropertyName("titre")]
        private string Title { get; set; }
        
        [JsonPropertyName("duree")]
        private long Duration { get; set; }
        
        [JsonPropertyName("realisateur")]
        private string DirectorName { get; set; }
        
        [JsonPropertyName("scenariste")]
        private string ScenaristName { get; set; }
        
        [JsonPropertyName("support")]
        private string Support { get; set; }
        
        [JsonPropertyName("genre")]
        private string Genre { get; set; }
        
        [JsonPropertyName("acteur-principal")]
        private string FirstActorName { get; set; }
        
        [JsonPropertyName("age-rating")]
        private string AgeRating { get; set; }
        
    }
}