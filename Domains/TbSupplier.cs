using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbSupplier
{
    [ValidateNever]
    public int SupplierId { get; set; }
    [Required(ErrorMessage ="Please Enter Supplier Name")]
    [MaxLength(50,ErrorMessage ="Please Enter in maximam 50 character")]
    public string SupplierName { get; set; } = "";
    [Required(ErrorMessage ="Please Enter The Phone Number")]
    [MaxLength(16,ErrorMessage ="Please Enter in Maxmam 16 number")]
    public string PhoneNumber { get; set; } = "";

    public int CurrentState { get; set; }

    public virtual ICollection<TbPurchaseInvoice> TbPurchaseInvoices { get; set; } = new List<TbPurchaseInvoice>();
}
