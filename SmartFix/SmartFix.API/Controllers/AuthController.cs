using Microsoft.AspNetCore.Mvc;
using SmartFix.Application.Interfaces;
using System.Threading.Tasks;

namespace SmartFix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // تسجيل مستخدم جديد
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto.Username, dto.Email, dto.Password, dto.Role, dto.PhoneNumber);
            if (user == null)
            {
                return BadRequest(new { message = "فشل التسجيل، البريد الإلكتروني مستخدم مسبقاً." });
            }

            return Ok(new { message = "تم إنشاء الحساب بنجاح", userId = user.Id, email = user.Email });
        }

        // تسجيل الدخول
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.LoginAsync(dto.Email, dto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "البريد الإلكتروني أو كلمة المرور غير صحيحة." });
            }

            return Ok(new { message = "تم تسجيل الدخول بنجاح", userId = user.Id, username = user.Username, role = user.Role });
        }
    }

    public record RegisterDto(string Username, string Email, string Password, string Role, string? PhoneNumber);
    public record LoginDto(string Email, string Password);
}
