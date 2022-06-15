using EventScape.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventScape.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


        [Required]
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal OrderTotal { get; set; }      
        public string BookingStatus { get; set; }      
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }

    }
}
