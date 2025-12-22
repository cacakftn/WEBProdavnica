using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        AuthResponse? Register(RegisterRequest request);
        AuthResponse? Login(LoginRequest request);
        AuthResponse? RefreshToken(string refreshToken);
        User? GetUserById(int userId);
        bool ChangePassword(int userId, string oldPassword, string newPassword);

    }
}
