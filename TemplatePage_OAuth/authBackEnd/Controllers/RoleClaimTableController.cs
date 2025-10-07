using authbackend.Dtos;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using authbackend.Entities;
using authBackEnd.Extensions;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("roleclaimtable")]
    public class RoleClaimTableController : ControllerBase
    {
        private readonly IRepository<RoleClaim, int> _dbRepository;

        public RoleClaimTableController(IRepository<RoleClaim, int> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult<IEnumerable<TablaRoleClaimDto>>> GetRoleClaimTable()
        {
            var roleClaims = await _dbRepository.GetQueryable()
            .Select(r => new TablaRoleClaimDto
            {
                Id = r.Key,
                RoleId = r.RoleId,
                Role = r.Role.Name,
                ClaimType = r.ClaimType,
                ClaimValue = r.ClaimValue
            }).ToListAsync(); 

            return Ok(roleClaims);
        }

        [HttpPut("table/update/{id}")]
        public async Task<IActionResult> UpdateRoleClaimTable(int id, [FromBody] string jsonValues)
        {
            var roleClaim = await _dbRepository.GetByIdAsync(id);

            if (roleClaim == null)
                return NotFound();

            JsonConvert.PopulateObject(jsonValues, roleClaim);

            if (!TryValidateModel(roleClaim))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _dbRepository.UpdateAsync(roleClaim);
            await _dbRepository.SaveChangesAsync();

            return Ok(roleClaim);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateRoleClaimTable(RoleClaim newRoleClaim)
        {
            bool exists = await _dbRepository.GetQueryable()
                .AnyAsync(u => u.RoleId == newRoleClaim.RoleId);

            if (exists)
            {
                return BadRequest("Id del rol ya está registrado.");
            }

            await _dbRepository.InsertAsync(newRoleClaim);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteRoleClaimTable(int id)
        {
            var roleClaim = await _dbRepository.GetByIdAsync(id);

            if (roleClaim == null)
            {
                return NotFound(); // Devuelve 404 si el rol no se encuentra.
            }

            await _dbRepository.DeleteAsync(roleClaim);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
