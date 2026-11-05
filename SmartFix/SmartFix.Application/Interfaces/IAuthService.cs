using System.Threading.Tasks;
using SmartFix.Domain.Entities;

namespace SmartFix.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string username, string email, string password, string role, string? phoneNumber);
        Task<User?> LoginAsync(string email, string password);
        Task<User?> GetUserByIdAsync(int userId);
    }
}
