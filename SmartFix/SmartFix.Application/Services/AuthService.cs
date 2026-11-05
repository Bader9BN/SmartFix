using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Interfaces;
using SmartFix.Domain.Entities;
using SmartFix.Infrastructur.Data;

namespace SmartFix.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SmartFixDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(SmartFixDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User?> RegisterAsync(string username, string email, string password, string role, string? phoneNumber)
        {
            // التحقق من عدم تكرار البريد الإلكتروني
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return null;
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Role = role,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            // تشفير كلمة المرور بأمان تام باستخدام PasswordHasher
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !user.IsActive)
            {
                return null;
            }

            // التحقق من صحة كلمة المرور المشفرة
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
