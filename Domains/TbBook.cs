using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models;

public partial class TbBook
{
    public TbBook()
    {
        TbPurchaseInvoiceBooks = new List<TbPurchaseInvoiceBook>();
        TbSalesInvoiceBooks = new List<TbSalesInvoiceBook>();
    }
    [ValidateNever]
    public int BookId { get; set; }
    [Required(ErrorMessage ="Please Enter The Title")]
    public string Title { get; set; } 

    public int AuthorId { get; set; }
    [Required(ErrorMessage = "Please Enter Number Of Pages")]
    public int NumberOfPages { get; set; }
    [Required(ErrorMessage = "Please Enter The Isbn")]
    [MaxLength(50,ErrorMessage ="Please Enter less than 50 Character")]
    //[Remote(action: "VerifyIsbn", controller: "Book", ErrorMessage = "it is found")]
    public string Isbn { get; set; }
    [Required(ErrorMessage = "Please Enter SalesPrice")]
    [Range(0, 5000, ErrorMessage = "Please Enter in Range from 0 to 5000")]
    public decimal SalesPrice { get; set; }
    [Required(ErrorMessage = "Please Enter PurchasePrice")]
    [Range(0, 5000, ErrorMessage = "Please Enter in Range from 0 to 5000")]
    public decimal PurchasePrice { get; set; }

    public int CurrentState { get; set; }

    public int CategoryId { get; set; }
    [ValidateNever]
    public string ImageName { get; set; }
    [MaxLength(4000,ErrorMessage ="please Enter in maxmam 4000 character")]
    public string? Description { get; set; }

    public int DiscountId { get; set; }

    public int PublishId { get; set; }

    public string? UpdateBy { get; set; } = null;

    public DateTime? UpdateDate { get; set; } = null;
    [ValidateNever]
    public string CreatedBy { get; set; }
    [ValidateNever]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int Qty { get; set; }
    [Range(1800, 2050, ErrorMessage = "Please Enter in Range from 1800 to 2050")]
    public string PublishYear { get; set; }
    [ValidateNever]
    public virtual TbAuthor Author { get; set; }
    [ValidateNever]
    public virtual TbCategory Category { get; set; }
    [ValidateNever]
    public virtual TbDiscount Discount { get; set; }
    [ValidateNever]
    public virtual TbPublish Publish { get; set; } 

    public virtual ICollection<TbPurchaseInvoiceBook> TbPurchaseInvoiceBooks { get; set; } 

    public virtual ICollection<TbSalesInvoiceBook> TbSalesInvoiceBooks { get; set; } 
}
