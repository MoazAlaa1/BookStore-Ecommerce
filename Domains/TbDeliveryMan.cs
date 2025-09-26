using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace BookStore.Models;

public partial class TbDeliveryMan
{
    [ValidateNever]
    public int DeliveryManId { get; set; }

    public string DeliveryManName { get; set; } = null!;

    public string? Address { get; set; }

    public int? Age { get; set; }
    //Salary
    public decimal Budget { get; set; }

    public int CurrentState { get; set; }

    public int Status { get; set; }
    [ValidateNever]
    public virtual ICollection<TbSalesInvoice> TbSalesInvoices { get; set; } = new List<TbSalesInvoice>();
}
