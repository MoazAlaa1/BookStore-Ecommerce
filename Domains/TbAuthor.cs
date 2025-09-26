using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbAuthor
{
    public TbAuthor()
    {
        
    }
    [ValidateNever]
    public int AuthorId { get; set; }
    [Required(ErrorMessage = "Please Enter Author Name")]
    [MaxLength(50, ErrorMessage = "Please Enter in maximam 50 character")]
    public string AuthorName { get; set; }
    [MaxLength(4000, ErrorMessage = "Please Enter in maximam 4000 character")]
    public string AuthorInfo { get; set; }

    public int CurrentState { get; set; }

    public virtual ICollection<TbBook> TbBooks { get; set; } = new List<TbBook>();
}
