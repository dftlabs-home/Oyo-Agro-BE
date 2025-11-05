using System.ComponentModel.DataAnnotations;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class ForgotPasswordParam
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; } = null!;
    }
}
