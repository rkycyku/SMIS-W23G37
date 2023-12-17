using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace W23G37.Auth
{
    public class LogInModelAPI
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
