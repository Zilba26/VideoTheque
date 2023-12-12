using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class FilmViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("titre")]
        public string Title { get; set; }
        
        [JsonPropertyName("duree")]
        public long Duration { get; set; }
        
        [JsonPropertyName("realisateur")]
        public string DirectorName { get; set; }
        
        [JsonPropertyName("scenariste")]
        public string ScenaristName { get; set; }
        
        [JsonPropertyName("support")]
        public string Support { get; set; }
        
        [JsonPropertyName("genre")]
        public string Genre { get; set; }
        
        [JsonPropertyName("acteur-principal")]
        public string FirstActorName { get; set; }
        
        [JsonPropertyName("age-rating")]
        public string AgeRating { get; set; }
        
        public FilmDto ToDto()
        {
            return new FilmDto
            {
                Id = Id,
                Title = Title,
                Duration = Duration,
                Director = new PersonneDto
                {
                    LastName = DirectorName.Split(" ")[0],
                    FirstName = DirectorName.Split(" ")[1]
                },
                Scenarist = new PersonneDto
                {
                    LastName = ScenaristName.Split(" ")[0],
                    FirstName = ScenaristName.Split(" ")[1]
                },
                Support = ViewModels.Support.BluRay,
                Genre = new GenreDto
                {
                    Name = Genre
                },
                FirstActor = new PersonneDto
                {
                    LastName = FirstActorName.Split(" ")[0],
                    FirstName = FirstActorName.Split(" ")[1]
                },
                AgeRating = new AgeRatingDto
                {
                    Name = AgeRating
                }
            };
        }
        
        public static FilmViewModel ToModel(FilmDto dto)
        {
            return new FilmViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Duration = dto.Duration,
                DirectorName = dto.Director.LastName + " " + dto.Director.FirstName,
                ScenaristName = dto.Scenarist.LastName + " " + dto.Scenarist.FirstName,
                Support = dto.Support.ToString(),
                Genre = dto.Genre.Name,
                FirstActorName = dto.FirstActor.LastName + " " + dto.FirstActor.FirstName,
                AgeRating = dto.AgeRating.Name
            };
        }
        
    }
}