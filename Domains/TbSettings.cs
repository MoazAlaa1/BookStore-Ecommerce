
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class TbSettings
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter WebsiteName")]
        public string WebsiteName { get; set; }
        [ValidateNever]
        public string Logo { get; set; } = "";
        [Required(ErrorMessage = "Please Enter Descripion")]
        public string WebsiteDescription { get; set; }
        [Url(ErrorMessage ="Please Enter a Valide URL")]
        public string FacebookLink { get; set; } = string.Empty;
        [Url(ErrorMessage = "Please Enter a Valide URL")]
        public string TwitterLink { get; set; } = string.Empty;
        [Url(ErrorMessage = "Please Enter a Valide URL")]
        public string InstgramLink { get; set; } = string.Empty;
        [Url(ErrorMessage = "Please Enter a Valide URL")]
        public string YoutubeLink { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        [ValidateNever]
        public string MiddlePanner { get; set; } = "";
        [ValidateNever]
        public string LastPanner { get; set; } = "";
    }
}
