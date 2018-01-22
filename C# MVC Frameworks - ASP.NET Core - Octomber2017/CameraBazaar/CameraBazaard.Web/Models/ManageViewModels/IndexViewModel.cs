namespace CameraBazaar.Web.Models.ManageViewModels
{
    using CameraBazaar.Services.Models.Cameras;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel 
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public IEnumerable<CameraListingModel> Cameras { get; set; }
    }
}
