using EventScape.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventScape.Models
{
    public class WishList
    {
        public WishList()
        {
            Tickets = 1;
        }
        [Key]
        public int WishListId { get; set; }     
      
        public int EventId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
     
        
        [ValidateNever]
        [ForeignKey("EventId")]
        public virtual Events? Event { get; set; }
        //public ICollection<Events> Events { get; set; }

        [Range(1, 10, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Tickets { get; set; }
        public DateTime DateCreated {get;set;}

        [NotMapped]
        public decimal Price { get; set; }
    }
}
