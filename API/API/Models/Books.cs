using API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("books")]
    public class Books : BaseEntity
    {
        [Required]
        [Column("title", TypeName = "Varchar(Max)")]
        public string Title { get; set; }

        [Required]
        [Column("author", TypeName = "Varchar(Max)")]
        public string Author { get; set; }

        [Required]
        [Column("price", TypeName = "Decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("launch_date", TypeName = "DateTime2(6)")]
        public DateTime LaunchDate { get; set; }  
    }
}
