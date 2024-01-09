using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.Core;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class GenreViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        [Required]
        public string Name { get; set; }
        
        public GenreDto ToDto()
        {
            return new GenreDto()
            {
                Name = Name
            };
        }
        
        public static GenreViewModel FromDto(GenreDto? genreDto)
        {
            if (genreDto == null)
            {
                throw new NotFoundException("Genre not found");
            }
            return new GenreViewModel()
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };
        }
    }
}
