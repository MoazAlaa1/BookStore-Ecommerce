using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbPublish
{
    [ValidateNever]
    public int PublishId { get; set; }
    [Required(ErrorMessage = "Please Enter Publisher Name")]
    [MaxLength(50, ErrorMessage = "Please Enter in maximam 50 character")]
    public string? Publisher { get; set; } = string.Empty;
    public int CurrentState { get; set; }
    [ValidateNever]
    public virtual ICollection<TbBook> TbBooks { get; set; } = new List<TbBook>();
}
