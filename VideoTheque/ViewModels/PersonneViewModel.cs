using System.Text.Json.Serialization;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class PersonneViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("nom")]
        public string LastName { get; set; }
        
        [JsonPropertyName("prenom")]
        public string FirstName { get; set; }
        
        [JsonPropertyName("nationalite")]
        public string Nationality { get; set; }
        
        [JsonPropertyName("date-naissance")]
        public DateTime Birthday { get; set; }
        
        [JsonPropertyName("nom-prenom")]
        public string FullName => $"{LastName} {FirstName}";

        public PersonneDto ToDto()
        {
            return new PersonneDto
            {
                Id = Id,
                BirthDay = Birthday,
                FirstName = FirstName,
                LastName = LastName,
                Nationality = Nationality
            };
        }

        public static PersonneViewModel FromDto(PersonneDto? personneDto)
        {
            if (personneDto == null)
            {
                throw new Exception("PersonneDto is null");
            }
            return new PersonneViewModel
            {
                Id = personneDto.Id,
                Birthday = personneDto.BirthDay,
                FirstName = personneDto.FirstName,
                LastName = personneDto.LastName,
                Nationality = personneDto.Nationality
            };
        }
    }
}