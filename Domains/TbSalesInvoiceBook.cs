using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class TbSalesInvoiceBook
{
    [ValidateNever]
    public int InvoiceBookId { get; set; }

    public int BookId { get; set; }

    public int InvoiceId { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public virtual TbBook Book { get; set; } = null!;

    public virtual TbSalesInvoice Invoice { get; set; } = null!;
}
