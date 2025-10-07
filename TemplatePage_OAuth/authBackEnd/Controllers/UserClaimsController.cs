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
    [Route("userclaims")]
    public class UserClaimsController : ControllerBase
    {
        private readonly IRepository<UserClaim, int> _userClaimRepository;

        public UserClaimsController(IRepository<UserClaim, int> userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult> GetUserClaimsTable()
        {
            var userClaims = await _userClaimRepository.GetQueryable().Include(c => c.User).Select(o => new UserClaimsDto{
                Id = o.Id, 
                UserId = o.UserId,
                Names = o.User != null ? o.User.Names : "",
                LastName = o.User != null ? o.User.LastName : "",
                SecondLastName = o.User != null ? o.User.SecondLastName : "",
                ClaimType = o.ClaimType,
                ClaimValue = o.ClaimValue 
            }).ToListAsync();
            return Ok(userClaims);
        }

        [HttpPut("table/update/{Id}")]
        public async Task<IActionResult> UpdateUserClaimsTable(int Id, [FromBody] string jsonValues)
        {
            var userClaim = await _userClaimRepository.GetQueryable().FirstOrDefaultAsync(uc => uc.Id == Id);

            if (userClaim == null)
            {
                return NotFound("UserClaim not found.");
            }

            JsonConvert.PopulateObject(jsonValues, userClaim);

            if (!TryValidateModel(userClaim))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _userClaimRepository.UpdateAsync(userClaim);
            await _userClaimRepository.SaveChangesAsync();

            return Ok(userClaim);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateUserClaimsTable(UserClaim newUserClaim)
        {
            bool exists = await _userClaimRepository.GetQueryable().AnyAsync(uc => uc.Id == newUserClaim.Id);

            if (exists)
            {
                return BadRequest("Id del UserClaim ya está registrado.");
            }

            await _userClaimRepository.InsertAsync(newUserClaim);
            await _userClaimRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteUserClaimsTable(int id)
        {
            var userClaim = await _userClaimRepository.GetByIdAsync(id);

            if (userClaim == null)
            {
                return NotFound(); // Devuelve 404 si el UserClaim no se encuentra.
            }

            await _userClaimRepository.DeleteAsync(userClaim); // Elimina el UserClaim.
            await _userClaimRepository.SaveChangesAsync(); // Guarda los cambios en la base de datos.
            return Ok();
        }
    }
}
