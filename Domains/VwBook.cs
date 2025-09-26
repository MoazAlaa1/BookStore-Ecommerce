using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class VwBook
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public int NumberOfPages { get; set; }

    public string Isbn { get; set; } = null!;

    public decimal SalesPrice { get; set; }

    public decimal PurchasePrice { get; set; }

    public int CurrentState { get; set; }

    public int CategoryId { get; set; }

    public string ImageName { get; set; } = null!;

    public string? Description { get; set; }

    public int DiscountId { get; set; }

    public int PublishId { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int Qty { get; set; }

    public string? AuthorName { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? DiscountPercent { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? PublishYear { get; set; }

    public string? Publisher { get; set; }
}
