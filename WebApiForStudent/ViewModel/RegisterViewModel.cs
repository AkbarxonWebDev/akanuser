using System.ComponentModel.DataAnnotations;


namespace WebApiForStudent.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ComfirmPassword { get; set; }
    }
}
