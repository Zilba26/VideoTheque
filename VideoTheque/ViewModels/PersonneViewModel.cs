using Newtonsoft.Json;
using VideoTheque.DTOs;

namespace VideoTheque.ViewModels
{
    public class PersonneViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("lastname")]
        public string Lastname { get; set; }
        
        [JsonProperty("firstname")]
        public string Firstname { get; set; }
        
        [JsonProperty("nationality")]
        public string Nationality { get; set; }
        
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        


        public PersonneDto ToDto()
        {
            return new PersonneDto
            {
                Id = this.Id,
                LastName = this.Lastname,
                FirstName = this.Firstname,
                Nationality = this.Nationality,
                BirthDay = this.Birthday,
            };
        }

        public static PersonneViewModel ToModel(PersonneDto dto)
        {
            return new PersonneViewModel
            {
                Id = dto.Id,
                Lastname = dto.LastName,
                Firstname = dto.FirstName,
                Birthday = dto.BirthDay,
                Nationality = dto.Nationality
            };
        }
    }
}