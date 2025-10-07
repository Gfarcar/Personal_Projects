using Microsoft.AspNetCore.Mvc;
using authbackend.Entities;
using authbackend.Interface;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("usertokens")]
    public class UserTokensController : ControllerBase
    {
        private readonly IRepository<UserToken, int> _userTokenRepository;

        public UserTokensController(IRepository<UserToken, int> userTokenRepository)
        {
            _userTokenRepository = userTokenRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult> GetUserTokensTable()
        {
            var userTokens = await _userTokenRepository.GetAllAsync();
            return Ok(userTokens);
        }

        [HttpDelete("table/delete/{UserId}")]
        public async Task<ActionResult> DeleteUserTokensTable(int UserId)
        {
            var deleted = await _userTokenRepository.DeleteAsync(UserId);

            if (!deleted)
            {
                return NotFound(); // Devuelve 404 si el UserToken no se encuentra.
            }

            await _userTokenRepository.SaveChangesAsync(); // Guarda los cambios en la base de datos.
            return Ok();
        }
    }
}