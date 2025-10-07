using authbackend.Dtos;
using authbackend.Entities;
using authbackend.Interface;
using authBackEnd.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("role")]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role, int> _dbRepository;

        public RoleController(IRepository<Role, int> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleTable()
        {
            var roles = await _dbRepository.GetQueryable().Select(r => new RoleDto{
                Id = r.Id,
                Name = r.Name 
            }).ToListAsync(); 
            
            return Ok(roles);
        }

        [HttpPut("table/update/{id}")]
        public async Task<IActionResult> UpdateRoleTable(int id, [FromBody] string jsonValues)
        {
            var role = await _dbRepository.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            JsonConvert.PopulateObject(jsonValues, role);

            if (!TryValidateModel(role))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _dbRepository.UpdateAsync(role);
            await _dbRepository.SaveChangesAsync();

            return Ok(role);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateRoleTable(Role newRole)
        {
            bool exists = await _dbRepository.GetQueryable().AnyAsync(u => u.Id == newRole.Id);

            if (exists)
            {
                return BadRequest("Id del rol ya está registrado.");
            }

            await _dbRepository.InsertAsync(newRole);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteRoleTable(int id)
        {
            var role = await _dbRepository.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound(); // Devuelve 404 si el rol no se encuentra.
            }

            await _dbRepository.DeleteAsync(role);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
