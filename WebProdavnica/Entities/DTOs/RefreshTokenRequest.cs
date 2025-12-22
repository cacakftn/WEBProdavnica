using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Refresh token je obavezan")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
