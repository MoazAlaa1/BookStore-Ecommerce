using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class TbSalesInvoice
{
    [ValidateNever]
    public int InvoiceId { get; set; }
    [ValidateNever]
    public DateTime InvoiceDate { get; set; }
    
    public decimal TotalPrice { get; set; }

    public int CurrentState { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CustomerId { get; set; } = "1";

    public string? Note { get; set; }

    public int CustomerDeliverId { get; set; }

    public int DeliveryManId { get; set; } 

    public string CreatedBy { get; set; } = null!;

    public string DeliveryState { get; set; } = null!;
    [ValidateNever]
    public DateTime DeliveryDate { get; set; }
    [ValidateNever]
    public virtual TbCustomerDeliverInfo CustomerDeliver { get; set; } = null!;
    [ValidateNever]
    public virtual TbDeliveryMan DeliveryMan { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<TbSalesInvoiceBook> TbSalesInvoiceBooks { get; set; } = new List<TbSalesInvoiceBook>();
}
