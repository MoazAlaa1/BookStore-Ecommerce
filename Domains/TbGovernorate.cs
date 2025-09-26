using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbGovernorate
{
    public TbGovernorate()
    {
        
    }
    [ValidateNever]
    public int GovernorateId { get; set; }
    [Required(ErrorMessage = "Please Enter Governorate Name")]
    [MaxLength(50, ErrorMessage = "Please Enter in maximam 50 character")]
    public string GovernorateName { get; set; } = null!;
    [Required(ErrorMessage = "Please Enter Delivery")]
    [DataType(DataType.Currency)]
    public decimal DeliveryPrice { get; set; }

    public int CurrentState { get; set; }
 
    public virtual ICollection<TbCustomerDeliverInfo> TbCustomerDeliverInfos { get; set; } = new List<TbCustomerDeliverInfo>();
}
