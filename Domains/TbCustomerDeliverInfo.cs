using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbCustomerDeliverInfo
{
    public TbCustomerDeliverInfo()
    {
        TbSalesInvoices = new List<TbSalesInvoice>();
    }
    [ValidateNever]
    public int CustomerDeliverId { get; set; }
    [Required(ErrorMessage ="Please Enter Name")]
    [MaxLength(50,ErrorMessage ="Enter character less than 50")]
    public string CutomerName { get; set; } = "";
    [Required(ErrorMessage = "Please Enter Address")]
    [MaxLength(50, ErrorMessage = "Enter character less than 50")]
    public string Adress { get; set; } = "";
    [Required(ErrorMessage = "Please Enter Phone Number")]
    [MaxLength(16, ErrorMessage = "Enter character less than 16")]
    public string PhoneNumber { get; set; } = "";
    [ValidateNever]
    public string UserId { get; set; }

    public int GovernorateId { get; set; }
    [ValidateNever]
    public virtual TbGovernorate Governorate { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<TbSalesInvoice> TbSalesInvoices { get; set; }
}
