using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EzFitAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace EzFitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComidaController : ControllerBase
    {
        private readonly EzFitContext dbContext;

        public ComidaController(EzFitContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [Route("/Lista")]
        public async Task<IActionResult> Get()
        {
            var listaComida = await dbContext.IngedientesComida.ToListAsync();

            return StatusCode(StatusCodes.Status200OK, listaComida);

        }

    }
}
