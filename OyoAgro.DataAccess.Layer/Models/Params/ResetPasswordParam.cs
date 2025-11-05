using System.ComponentModel.DataAnnotations;

namespace OyoAgro.DataAccess.Layer.Models.Params
{
    public class ResetPasswordParam
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
