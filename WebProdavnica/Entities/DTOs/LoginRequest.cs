using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username je obavezan")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password je obavezan")]
        public string Password { get; set; } = string.Empty;
    }
}
