using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ShoppingCartBook
    {
        [ValidateNever]
        public int? InvoiceBookId { get; set; } = null!;
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter Quantity")]
        public int SalesQty { get; set; }
        public int BookQty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set;}

    }
}
