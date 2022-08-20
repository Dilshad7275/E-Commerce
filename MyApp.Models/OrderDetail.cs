using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyApp.Models
{
    public class OrderDetail
    {
        
        public int Id { get; set; }
        [Required]
        public string OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader  OrderHeader { get; set; }  
        [Required]
      
        public int   ProductId { get; set; }
        [ValidateNever]

        public Product Product{get;set;}
        public double Price{get;set;}
        public int Count{get;set;}
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
