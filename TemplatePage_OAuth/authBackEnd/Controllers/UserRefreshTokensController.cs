using Microsoft.AspNetCore.Mvc;
using authbackend.Entities;
using authbackend.Interface;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("userrefreshtokens")]
    public class UserRefreshTokensController : ControllerBase
    {
        private readonly IRepository<UserRefreshToken, int> _userRefreshTokenRepository;

        public UserRefreshTokensController(IRepository<UserRefreshToken, int> userRefreshTokenRepository)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult> GetUserRefreshTokensTable()
        {
            var userRefreshTokens = await _userRefreshTokenRepository.GetAllAsync();
            return Ok(userRefreshTokens);
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteUserRefreshTokensTable(int id)
        {
            var deleted = await _userRefreshTokenRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(); // Returns 404 if the UserRefreshToken is not found.
            }

            await _userRefreshTokenRepository.SaveChangesAsync(); // Saves changes to the database.
            return Ok();
        }
    }
}