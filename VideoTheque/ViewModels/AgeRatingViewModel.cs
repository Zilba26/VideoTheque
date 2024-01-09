using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.Core;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class AgeRatingViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("nom")]
        [Required]
        public string Name { get; set; }
        
        [JsonPropertyName("abreviation")]
        [Required]
        public string Abreviation { get; set; }
        
        public AgeRatingDto ToDto()
        {
            return new AgeRatingDto()
            {
                Name = Name,
                Abreviation = Abreviation
            };
        }
        
        public static AgeRatingViewModel FromDto(AgeRatingDto? ageRatingDto)
        {
            if (ageRatingDto == null)
            {
                throw new NotFoundException("Age rating not found");
            }
            return new AgeRatingViewModel()
            {
                Id = ageRatingDto.Id,
                Name = ageRatingDto.Name,
                Abreviation = ageRatingDto.Abreviation
            };
        }   
    }
}