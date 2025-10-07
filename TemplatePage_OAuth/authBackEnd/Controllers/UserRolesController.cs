using Microsoft.AspNetCore.Mvc;
using authbackend.Entities;
using authbackend.Interface;
using authBackEnd.Extensions;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using authbackend.Dtos;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("userroles")]
    public class UserRolesController : ControllerBase
    {
        private readonly IRepository<UserRole, (string UserId, string RoleId)> _userRoleRepository;

        public UserRolesController(IRepository<UserRole, (string UserId, string RoleId)> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult> GetUserRolesTable()
        {
            var userRoles = await _userRoleRepository.GetQueryable().Include(r => r.User).Include(r => r.Role).Select(o => new UserRolesDto{
                Id = o.Key,
                UserId = o.UserId,
                Names = o.User != null ? o.User.Names : "",
                LastName = o.User != null ? o.User.LastName : "",
                SecondLastName = o.User != null ? o.User.SecondLastName : "",
                RoleId = o.RoleId, 
                RoleName = o.Role.Name 
            }).ToListAsync();
            return Ok(userRoles);
        }

        [HttpPut("table/update/{Id}")]
        public async Task<IActionResult> UpdateUserRolesTable(int Id, [FromBody] string jsonValues)
        {
            Console.WriteLine(jsonValues);

            var userRole = await _userRoleRepository.GetQueryable().FirstOrDefaultAsync(ur => ur.UserId == Id);

            if (userRole == null)
            {
                return NotFound();
            }

            JsonConvert.PopulateObject(jsonValues, userRole);

            if (!TryValidateModel(userRole))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _userRoleRepository.UpdateAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();
            return Ok(userRole);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateUserRolesTable(UserRole newUserRole)
        {
            bool exists = await _userRoleRepository.GetQueryable().AnyAsync(ur => ur.UserId == newUserRole.UserId && ur.RoleId == newUserRole.RoleId);

            if (exists)
            {
                return BadRequest("Esta relación de Usuario-Rol ya está registrada.");
            }

            await _userRoleRepository.InsertAsync(newUserRole);
            await _userRoleRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("table/delete/{UserId}/{RoleId}")]
        public async Task<ActionResult> DeleteUserRolesTable(string UserId, string RoleId)
        {
            var deleted = await _userRoleRepository.DeleteAsync((UserId, RoleId));

            if (!deleted)
            {
                return NotFound(); // Devuelve 404 si la relación UserRole no se encuentra.
            }

            await _userRoleRepository.SaveChangesAsync(); // Guarda los cambios en la base de datos.
            return Ok();
        }
    }
}