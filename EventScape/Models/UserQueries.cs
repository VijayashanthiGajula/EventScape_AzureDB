using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventScape.Models
{
    public class UserQueries
    {

        [Key]
        public int ID { get; set; }
        public int EventId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        [ForeignKey("EventId")]
        public Events? Events { get; set; }

        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        [Required]
        [StringLength(1000)]

        public string Query { get; set; } = null!;

        public DateTime DatePosted { get; set; }
        [ValidateNever]
        [StringLength(1000)]
        public string Reply { get; set; } 
        public string Status { get; set; } = null!;
         

      
        public UserQueries()
        {


        }
    }
}
