using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            lstBooks = new List<ShoppingCartBook>();
        }
        public List<ShoppingCartBook> lstBooks { get; set; }
        public int invoiceId { get; set; }
        public int CustomerDeliverId { get; set; }
        [ValidateNever]
        public decimal SubTotal { get; set; }
        [ValidateNever]
        public decimal Total { get; set; }
        [ValidateNever]
        public decimal ShippingCost { get; set; }
        [ValidateNever]
        public string PromoCode { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Enter character less than 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [MaxLength(16, ErrorMessage = "Enter character less than 16")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        [MaxLength(50, ErrorMessage = "Enter character less than 50")]
        public string Address { get; set; }
        public int GovernorateId { get; set; }
        public bool isNew { get; set; } = true;
    }
}
