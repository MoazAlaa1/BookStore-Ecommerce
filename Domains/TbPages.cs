using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class TbPages
    {
        public int PageId { get; set; }
        [Required(ErrorMessage ="Please Enter the title")]
        [MaxLength(50,ErrorMessage ="Please Enter less than 50 character")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter the Desctiption")]
        public string Description { get; set; }
        public string? MetaKeyWord { get; set; } = null!;
        public string? MetaDescriptiuon { get; set; } = null!;
        [ValidateNever]
        public string ImageName { get; set; } = null!;
        public int CurrentState { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
