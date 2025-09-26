using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore.Models
{
    public class PurchaseCartBook
    {
        [ValidateNever]
        public int? InvoiceBookId { get; set; } = null!;
        public int InvoiceId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public int PurchaseQty { get; set; }
        public decimal Price { get; set; }

    }
}
