using authbackend.Dtos;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using authbackend.Entities;
using authBackEnd.Extensions;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("userstable")]
    public class UsersTableController : ControllerBase
    {
        private readonly IRepository<User, int> _dbRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IRoleClaimAssignService _roleClaimAssignService;
        private readonly IMapper _mapper;

        public UsersTableController(
            IRepository<User, int> dbRepository,
            IPasswordHashingService passwordHashingService,
            IMapper mapper,
            IRoleClaimAssignService roleClaimAssignService)
        {
            _dbRepository = dbRepository;
            _passwordHashingService = passwordHashingService;
            _mapper = mapper;
            _roleClaimAssignService = roleClaimAssignService;
        }

        [HttpGet("table")]
        public async Task<ActionResult<IEnumerable<TablaEmpleadosBasicoDto>>> GetUsersTable()
        {
            var users = await _dbRepository.GetAllAsync();
            var tablaBasica = users.Select(u => new TablaEmpleadosBasicoDto
            {
                Id = u.Id,
                Names = u.Names,
                LastName = u.LastName,
                SecondLastName = u.SecondLastName,
                Email = u.Email
            });
            return Ok(tablaBasica);
        }

        [HttpPut("table/update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] string jsonValues)
        {
            var user = await _dbRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            JsonConvert.PopulateObject(jsonValues, user);

            if (!TryValidateModel(user))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _dbRepository.UpdateAsync(user);
            await _dbRepository.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateUserTable(RegisterUserDto newUser)
        {
            var existingUser = await _dbRepository.GetQueryable()
                .FirstOrDefaultAsync(u => u.Email == newUser.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already in use.");
            }

            newUser.Password = _passwordHashingService.HashPassword(newUser.Password);

            User user = _mapper.Map<User>(newUser);
            await _dbRepository.InsertAsync(user);
            await _dbRepository.SaveChangesAsync();
            await _roleClaimAssignService.AssignRoleToUser(user.Id, 3);
            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteUserTable(int id)
        {
            var result = await _dbRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            await _dbRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
