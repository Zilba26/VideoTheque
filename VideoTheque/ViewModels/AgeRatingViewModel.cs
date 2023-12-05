using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class AgeRatingViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("nom")]
        [Required]
        public string Name { get; set; }
        
        [JsonProperty("abreviation")]
        public string Abreviation { get; set; }

        public AgeRatingDto ToDto()
        {
            return new AgeRatingDto
            {
                Id = this.Id,
                Name = this.Name,
                Abreviation = this.Abreviation
            };
        }
        
        public static AgeRatingViewModel ToModel(AgeRatingDto dto)
        {
            return new AgeRatingViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Abreviation = dto.Abreviation
            };
        }
    }
}