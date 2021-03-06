namespace CameraBazaar.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
