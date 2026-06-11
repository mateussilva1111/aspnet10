using API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Person")]
    public class Person : BaseEntity
    {

        [Required]
        [Column("first_name", TypeName ="Varchar(80)")]
        [MaxLength(80)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name", TypeName = "Varchar(80)")]
        [MaxLength(80)]
        public string LastName { get; set; }

        [Required]
        [Column("address", TypeName = "Varchar(100)")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [Column("gender")]
        public string Gender { get; set; }
    }
}
