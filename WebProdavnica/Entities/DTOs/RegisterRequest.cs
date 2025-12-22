using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username je obavezan")]
        [MinLength(3, ErrorMessage = "Username mora imati najmanje 3 karaktera")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Nevažeća email adresa")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password je obavezan")]
        [MinLength(6, ErrorMessage = "Password mora imati najmanje 6 karaktera")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Potvrda passworda je obavezna")]
        [Compare("Password", ErrorMessage = "Passwordi se ne poklapaju")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
