
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public partial class TbSlider
    {
        public int SliderId { get; set; }
        [Required(ErrorMessage ="Please Enter The title")]
        [MaxLength(50,ErrorMessage ="Please Enter less than 50 Character")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please Enter The title")]
        [MaxLength(1000, ErrorMessage = "Please Enter less than 1000 Character")]
        public string? Description { get; set; }
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
