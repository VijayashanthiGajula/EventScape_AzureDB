using EventScape.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace EventScape.Models
{
    public class UserQueries
    {

        [Key]
        public int ID { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Query { get; set; } = null!;

        public DateTime DatePosted { get; set; }
        [Required]
        [StringLength(1000)]
        public string Reply { get; set; } = null!;
        public string Status { get; set; } = null!;
        [Required]

        public Events? Events { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public UserQueries()
        {


        }
    }
}
