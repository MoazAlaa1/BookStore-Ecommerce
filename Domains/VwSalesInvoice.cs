using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class VwSalesInvoice
{
    public int InvoiceId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public decimal TotalPrice { get; set; }

    public int CurrentState { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CustomerId { get; set; } = null!;

    public int CustomerDeliverId { get; set; }

    public int DeliveryManId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string DeliveryState { get; set; } = null!;

    public string CutomerName { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int GovernorateId { get; set; }

    public string GovernorateName { get; set; } = null!;

    public decimal DeliveryPrice { get; set; }

    public string DeliveryManName { get; set; } = null!;
}
