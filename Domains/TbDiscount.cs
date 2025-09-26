using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbDiscount
{
    [ValidateNever]
    public int DiscountId { get; set; }
    [Required(ErrorMessage ="Please Enter Discount Percent")]
    [Range(0,100,ErrorMessage ="Please Enter in Range from 0 to 100")]
    public int DiscountPercent { get; set; }
    [Required(ErrorMessage = "Please Enter Expiry Date")]
    [DataType(DataType.DateTime, ErrorMessage = "Please Enter a valid date")]
    public DateTime ExpiryDate { get; set; } = DateTime.Now.AddDays(5);
    public int CurrentState { get; set; }
    [ValidateNever]
    public virtual ICollection<TbBook> TbBooks { get; set; } = new List<TbBook>();
}
