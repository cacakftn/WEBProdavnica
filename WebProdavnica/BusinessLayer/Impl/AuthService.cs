using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DAL.Abstract;
using Entities;
using Entities.Configuration;
using Entities.DTOs;

namespace BusinessLayer.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IJwtService jwtService, IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public AuthResponse? Register(RegisterRequest request)
        {
            // Provera da li korisnik već postoji
            var existingUser = _userRepository.GetByUsername(request.Username);
            if (request.Username == existingUser.Username)
            {
                return null; 
            }

            var existingEmail = _userRepository.GetByEmail(request.Email);
            if (existingEmail.Email == request.Email)
            {
                return null; 
            }

            // Kreiranje novog korisnika
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Status = true,
                CreatedDate = DateTime.UtcNow,
                IdRole = 2 // Default role (User)
            };

            // Generisanje refresh tokena
            user.RefreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            var success = _userRepository.Add(user);
            if (!success)
            {
                return null;
            }

            // Dohvatanje kreiranog korisnika da bismo dobili IdUser
            var createdUser = _userRepository.GetByUsername(user.Username!);
            if (createdUser == null)
            {
                return null;
            }

            // Generisanje access tokena
            var accessToken = _jwtService.GenerateAccessToken(
                createdUser.IdUser,
                createdUser.Username!,
                createdUser.Email!,
                GetRoleName(createdUser.IdRole)
            );

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = createdUser.RefreshToken!,
                ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
                Username = createdUser.Username!,
                Email = createdUser.Email!,
                UserId = createdUser.IdUser,
                Role = GetRoleName(createdUser.IdRole)
            };
        }

        public AuthResponse? Login(LoginRequest request)
        {
            var user = _userRepository.GetByUsername(request.Username);
            if (user == null || !user.Status)
            {
                return null; // Korisnik ne postoji ili nije aktivan
            }

            // Provera passworda
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null; // Pogrešan password
            }

            // Generisanje novog refresh tokena
            user.RefreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            _userRepository.Update(user);

            // Generisanje access tokena
            var accessToken = _jwtService.GenerateAccessToken(
                user.IdUser,
                user.Username!,
                user.Email!,
                GetRoleName(user.IdRole)
            );

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
                Username = user.Username!,
                Email = user.Email!,
                UserId = user.IdUser,
                Role = GetRoleName(user.IdRole)
            };
        }

        public AuthResponse? RefreshToken(string refreshToken)
        {
            var user = _userRepository.GetByRefreshToken(refreshToken);
            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return null; // Refresh token je nevažeći ili istekao
            }

            // Generisanje novog refresh tokena
            user.RefreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            _userRepository.Update(user);

            // Generisanje novog access tokena
            var accessToken = _jwtService.GenerateAccessToken(
                user.IdUser,
                user.Username!,
                user.Email!,
                GetRoleName(user.IdRole)
            );

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                ExpiresIn = _jwtSettings.AccessTokenExpirationMinutes * 60,
                Username = user.Username!,
                Email = user.Email!,
                UserId = user.IdUser,
                Role = GetRoleName(user.IdRole)
            };
        }

        public User? GetUserById(int userId)
        {
            return _userRepository.Get(userId);
        }

        public bool ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return false;
            }

            // Provera starog passworda
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            {
                return false;
            }

            // Postavljanje novog passworda
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.Update(user);

            return true;
        }

        private string GetRoleName(int roleId)
        {
            return roleId switch
            {
                1 => "Admin",
                2 => "User",
                _ => "Guest"
            };
        }
    }
}
