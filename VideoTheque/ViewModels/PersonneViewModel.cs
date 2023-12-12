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
            Console.WriteLine("ToDto");
            Console.WriteLine(this.FirstName);
            return new PersonneDto
            {
                Id = this.Id,
                LastName = this.LastName,
                FirstName = this.FirstName,
                Nationality = this.Nationality,
                BirthDay = this.Birthday,
            };
        }

        public static PersonneViewModel ToModel(PersonneDto dto)
        {
            Console.WriteLine("ToModel");
            Console.WriteLine(dto.FirstName);
            return new PersonneViewModel
            {
                Id = dto.Id,
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Birthday = dto.BirthDay,
                Nationality = dto.Nationality
            };
        }
    }
}