using authbackend.Dtos;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using authbackend.Entities;
using Microsoft.AspNetCore.Authorization;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IRepository<UserToken, int> _userTokenRepository;
        private readonly IRepository<UserRefreshToken, int> _userRefreshTokenRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ItokenService _tokenService;
        private readonly IRoleClaimAssignService _roleClaimAssignService;
        private readonly IMapper _mapper;

        public UserController(
            IRepository<User, int> userRepository,
            IRepository<UserToken, int> userTokenRepository,
            IRepository<UserRefreshToken, int> userRefreshTokenRepository,
            IPasswordHashingService passwordHashingService,
            IMapper mapper,
            ItokenService tokenService,
            IRoleClaimAssignService roleClaimAssignService)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _passwordHashingService = passwordHashingService;
            _mapper = mapper;
            _tokenService = tokenService;
            _roleClaimAssignService = roleClaimAssignService;
        }

        // GET user/1 
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCreatedResponseDto>> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            UserCreatedResponseDto returnUser = _mapper.Map<UserCreatedResponseDto>(user);
            return Ok(returnUser);
        }

        // Signup
        [HttpPost("signup")]
        public async Task<ActionResult<GetUserResponseDto>> CreateUser(RegisterUserDto newUser)
        {
            bool emailExists = await _userRepository.GetQueryable().AnyAsync(u => u.Email == newUser.Email);

            if (emailExists)
            {
                return BadRequest("Email already in use.");
            }

            newUser.Password = _passwordHashingService.HashPassword(newUser.Password);
            User user = _mapper.Map<User>(newUser);

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveChangesAsync();
            await _roleClaimAssignService.AssignRoleToUser(user.Id, 3);

            var userToken = new UserToken
            {
                UserId = user.Id,
                Token = await _tokenService.CreateToken(user),
                Expiration = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            };

            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                Expiration = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            };

            await _userTokenRepository.UpdateAsync(userToken);
            await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
            await _userRepository.SaveChangesAsync();
            await _userTokenRepository.SaveChangesAsync();
            await _userRefreshTokenRepository.SaveChangesAsync();

            UserCreatedResponseDto returnUser = _mapper.Map<UserCreatedResponseDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, returnUser);
        }

        // Login  
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var existingUser = await _userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser == null)
                return BadRequest("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash))
            {
                return BadRequest("Wrong user or password");
            }

            string token = await _tokenService.CreateToken(existingUser);
            string refreshtoken = _tokenService.GenerateRefreshToken();

            var response = new LoginResponseDto{
                AccessToken = token, 
                RefreshToken = refreshtoken
            };

            var existingToken = await _userTokenRepository.GetByIdAsync(existingUser.Id);

            if (existingToken != null)
            {
                existingToken.Token = token;
                existingToken.Expiration = DateTime.UtcNow.AddDays(30);
                await _userTokenRepository.UpdateAsync(existingToken);
            }
            else
            {
                var userToken = new UserToken
                {
                    UserId = existingUser.Id,
                    Token = response.AccessToken,
                    Expiration = DateTime.UtcNow.AddDays(30),
                    IsRevoked = false
                };

                await _userTokenRepository.UpdateAsync(userToken);
            }

            await _userTokenRepository.SaveChangesAsync();

            var userRefreshToken = new UserRefreshToken
            {
                UserId = existingUser.Id,
                RefreshToken = response.RefreshToken,
                Expiration = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            };

            await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
            await _userRefreshTokenRepository.SaveChangesAsync();

            return Ok(response);
        }


         // Login  
        [Authorize]
        [HttpPost("my-account")]
        public async Task<IActionResult> MyAccount(RefreshTokenDto request)
        {       
            
            var existingUser = await _userRepository.GetQueryable()
            .Include(u => u.UserRoles)
            .Include(c => c.UserClaims)
            .Include(r => r.UserRefreshToken) // Incluye la relación con los tokens
            .FirstOrDefaultAsync(u => u.UserRefreshToken
            .Any(t => t.RefreshToken == request.RefreshToken));


            if (existingUser == null)
                return BadRequest("User not found.");

            var response = new MyAccountResponseDto
            {
                User = existingUser.Email, 
                Roles = existingUser.UserRoles.Select(r => r.RoleId).ToArray(), // Convierte los roles a string[]
                Claims = existingUser.UserClaims.Select(c => new ClaimDto  // Convierte los claims a ClaimDto[]
                {
                    Type = c.ClaimType,   // Asume que UserClaim tiene la propiedad ClaimType
                    Value = c.ClaimValue  // Asume que UserClaim tiene la propiedad ClaimValue
                }).ToArray()
            };

            return Ok(response);
        }
        


     // Logout
        [HttpPost("logout")]
        public async Task<IActionResult> UserLogout(UserLogoutDto request)
        {
            var existingUser = await _userRepository.GetQueryable().FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser == null)
            {
                return BadRequest("User not found.");
            }

            // Obtener el token de acceso del usuario
            var userToken = await _userTokenRepository.GetQueryable().FirstOrDefaultAsync(ut => ut.UserId == existingUser.Id);

            if (userToken != null)
            {
                await _userTokenRepository.DeleteAsync(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }

            // Obtener todos los tokens de actualización del usuario
            var userRefreshTokens = await _userRefreshTokenRepository.GetQueryable().Where(urt => urt.UserId == existingUser.Id).ToListAsync();

            if (userRefreshTokens.Count > 0)
            {
                await _userRefreshTokenRepository.DeleteRangeAsync(userRefreshTokens); // Asumiendo que tienes un método para eliminar múltiples
                await _userRefreshTokenRepository.SaveChangesAsync();
            }

            return Ok(new { message = "Logged out successfully." });
        }


        // GET /users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCreatedResponseDto>>> GetUsers()
        {
            var users = await _userRepository.GetQueryable()
                .Select(user => _mapper.Map<UserCreatedResponseDto>(user))
                .AsNoTracking()
                .ToListAsync();
            return Ok(users);
        }

        // Add roles
        [HttpPost("{userId}/roles/addrole/{roleId}")]
        public async Task<IActionResult> AddRole(int userId, int roleId)
        {
            await _roleClaimAssignService.AssignRoleToUser(userId, roleId);
            return Ok();
        }

        // Add Claim
        [HttpPost("{userId}/claims/addclaim/")]
        public async Task<IActionResult> AddClaim(int userId, [FromBody] ClaimDto claim)
        {
            await _roleClaimAssignService.AssignClaimToUser(userId, claim.Type, claim.Value);
            return Ok();
        }

        // Get user Role
        [HttpGet("{userId}/roles")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(int userId)
        {
            var user = await _userRepository.GetQueryable()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
            return Ok(roles);
        }

        // Get user Claims
        [HttpGet("{userId}/claims")]
        public async Task<ActionResult<IEnumerable<string>>> GetUserClaims(int userId)
        {
            var user = await _userRepository.GetQueryable()
                .Include(u => u.UserClaims)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            var claims = user.UserClaims.Select(uc => new ClaimDto
            {
                Type = uc.ClaimType,
                Value = uc.ClaimValue
            }).ToList();

            return Ok(claims);
        }

        // Admin endpoint
        [HttpGet("Admin")]
        [Authorize(Policy = "Admin")]
        public ActionResult<string> AdminTestEndpoint()
        {
            return Ok("Eres admin");
        }

        // Editor endpoint
        [HttpGet("Editor")]
        [Authorize(Policy = "Editor")]
        public ActionResult<string> EditorTestEndpoint()
        {
            return Ok("Eres Editor");
        }

        // Viewer endpoint
        [HttpGet("Viewer")]
        [Authorize(Policy = "Viewer")]
        public ActionResult<string> ViewerTestEndpoint()
        {
            return Ok("Eres Viewer");
        }

        // Moderator endpoint
        [HttpGet("Moderator")]
        [Authorize(Policy = "Moderator")]
        public ActionResult<string> ModeratorTestEndpoint()
        {
            return Ok("Eres Moderator");
        }

        // Test user
        [HttpPost("testlogin")]
        public async Task<ActionResult<IEnumerable<string>>> GetTestUser()
        {
            var user = await _userRepository.GetQueryable()
                .Where(u => u.Id == 1)
                .FirstOrDefaultAsync();

            if (user == null)
                return BadRequest();

            var testUser = new ReactTestDto
            {
                User = "testUser",
                Roles = new[] { "Admin", "User" },
                Claims = new[]
                {
                    new ClaimDto { Type = "Email", Value = "testuser@example.com" },
                    new ClaimDto { Type = "Permission", Value = "CanEdit" }
                },
                AccessToken = await _tokenService.CreateTemporalToken(user),
                RefreshToken = await _tokenService.CreateTemporalToken(user)
            };

            return Ok(testUser);
        }
    }
}
