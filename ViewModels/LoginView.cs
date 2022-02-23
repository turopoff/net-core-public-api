using System.ComponentModel.DataAnnotations;

namespace PrepTeach.ViewModels
{
    public class LoginView
    {
        [Required]
        [StringLength(20)]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Password { get; set; } = null!;
    }
}
