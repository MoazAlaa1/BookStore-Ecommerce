using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class TbPurchaseInvoice
{
    [ValidateNever]
    public int PurchaseInvoiceId { get; set; }
    [ValidateNever]
    public DateTime InvoiceDate { get; set; } 

    public decimal TotalPrice { get; set; }

    public int CurrentState { get; set; }

    public string UpdatedBy { get; set; } = "";

    public DateTime? UpdatedDate { get; set; } = DateTime.Now;

    public int SupplierId { get; set; }

    public string? Note { get; set; }
    [ValidateNever]
    public string CreatedBy { get; set; }
    [ValidateNever]
    public virtual TbSupplier Supplier { get; set; } = null!;

    public virtual ICollection<TbPurchaseInvoiceBook> TbPurchaseInvoiceBooks { get; set; } = new List<TbPurchaseInvoiceBook>();
}
