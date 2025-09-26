using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbCategory
{
    [ValidateNever]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Please Enter Category Name")]
    [MaxLength(50, ErrorMessage = "Please Enter in maximam 50 character")]
    public string? CategoryName { get; set; } = string.Empty;
    [ValidateNever]
    public string CreatedBy { get; set; } = "";
    [ValidateNever]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [ValidateNever]
    public string ImageName { get; set; } = "";

    public bool ShowInHomePage { get; set; } = false;

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CurrentState { get; set; }
    [ValidateNever]
    public virtual ICollection<TbBook> TbBooks { get; set; } = new List<TbBook>();
}
