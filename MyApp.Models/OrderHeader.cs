using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyApp.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser  ApplicationUser { get; set; }  
        public DateTime DateOfOrder { get; set; } = DateTime.Now;
        public DateTime DateOfShipping { get; set; } = DateTime.Now;
        public double   OrderTotal { get; set; }
        public string? OrderStatus{get;set;}
        public string? PaymentStatus{get;set;}
        public string? TrackingNumber{get;set;}
        public string? Carrier{get;set;}
        public string? SessionId{get;set;}
        public string? PaymentIntentId{get;set;}

        public string? DateOfPayment{get;set;}
        public string? DueDate{get;set;}
        [Required]
        public string Phone{get;set;}
        [Required]
        public string Address{get;set;}
        [Required]
        public string City{get;set;}
        [Required]
        public string State{get;set;}
        [Required]
        public string PostalCode{get;set;}
        [Required]
        public string Name{get;set;}






    }
}
