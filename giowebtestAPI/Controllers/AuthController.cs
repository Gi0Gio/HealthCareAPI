using Microsoft.AspNetCore.Mvc;
using giowebtestAPI.DTOs;
using giowebtestAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace giowebtestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Post: api/Auth/register Esto es para el User
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            // Chek if username or email, is already on database
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username || u.Email == registerDto.Email))
            {
                return Conflict("Papa, el usuario como que ya está, y si no, es el email el que ya existe.");
            }

            //Hash the password. :D
            var hashedPassword = HashPassword(registerDto.Password);

            //Create the new user
            var newUser = new User
            {
                Username = registerDto.Username,
                PasswordHash = hashedPassword,
                Email = registerDto.Email,
                RoleId = registerDto.RoleId,
                RegistrationDate = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                UserId = newUser.Id
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            // Find user by username or email
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == loginDto.UsernameOrEmail || u.Email == loginDto.UsernameOrEmail);

            // Validate user existence and password
            if (user == null || user.PasswordHash != HashPassword(loginDto.Password))
            {
                return Unauthorized("Invalid credentials! Please check your username/email and password.");
            }

            // Get role name based on RoleId
            var roleName = user.Role?.Name;
            int? roleSpecificId = null;
            string displayName = null;

            // Determine the specific ID for Doctor, Patient, or Clinic based on the role
            if (roleName == "Doctor")
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == user.Id);
                roleSpecificId = doctor?.Id;
                displayName = doctor?.FirstName;
            }
            else if (roleName == "Patient")
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);
                roleSpecificId = patient?.Id;
                displayName = patient?.FirstName;
            }
            else if (roleName == "Administrator")
            {
                var clinic = await _context.Clinics.FirstOrDefaultAsync(c => c.Id.Equals(user.Id));
                roleSpecificId = clinic?.Id;
                displayName = clinic?.Name;
            }

            if (roleSpecificId == null)
            {
                return BadRequest("Role-specific ID not found for the user.");
            }

            return Ok(new 
            {
                UserId = user.Id,
                RoleId = roleSpecificId,
                RoleName = roleName,
                DisplayName = displayName
            });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
