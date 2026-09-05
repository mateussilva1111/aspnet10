using API.JsonSerializer;
using API.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data.Dto
{
    public class PersonDTO : BaseEntity
    {
        [JsonPropertyOrder(1)]
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyOrder(2)]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyOrder(3)]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyOrder(4)]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyOrder(6)]
        [JsonConverter(typeof(GenderSerializer))]
        public string Gender { get; set; }

        //[JsonConverter(typeof(DateSerializer))]
        //public DateTime? BrithDay { get; set; }

        [Column("enabled")]
        public bool Enabled { get; set; }
    }
}
