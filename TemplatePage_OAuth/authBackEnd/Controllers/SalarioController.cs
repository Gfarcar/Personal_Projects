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
    [Route("salario")]
    public class SalarioController : ControllerBase
    {
        private readonly IRepository<Salario, int> _dbRepository;

        public SalarioController(IRepository<Salario, int> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet("table")]
        public async Task<ActionResult<IEnumerable<Salario>>> GetSalarioTable()
        {
            var salarios = await _dbRepository.GetQueryable().Include(u => u.User).Select(s => new SalarioDto
            {
                Id = s.Id,
                Monto = s.Monto,
                Fecha = s.Fecha,
                UserId = s.UserId,
                Names = s.User != null ? s.User.Names : "",
                LastName = s.User != null ? s.User.LastName : "",
                SecondLastName = s.User != null ? s.User.SecondLastName : "" 
            }).ToListAsync(); 
            return Ok(salarios);
        }

        [HttpPut("table/update/{id}")]
        public async Task<IActionResult> UpdateSalarioTable(int id, [FromBody] string jsonValues)
        {
            var salario = await _dbRepository.GetByIdAsync(id);

            if (salario == null)
                return NotFound();

            JsonConvert.PopulateObject(jsonValues, salario);

            if (!TryValidateModel(salario))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _dbRepository.UpdateAsync(salario);
            await _dbRepository.SaveChangesAsync();

            return Ok(salario);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateSalarioTable(Salario newSalario)
        {
            bool exists = await _dbRepository.GetQueryable()
                .AnyAsync(s => s.Id == newSalario.Id);

            if (exists)
            {
                return BadRequest("Id del salario ya está registrado.");
            }

            await _dbRepository.InsertAsync(newSalario);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteSalarioTable(int id)
        {
            var salario = await _dbRepository.GetByIdAsync(id);

            if (salario == null)
            {
                return NotFound(); // Devuelve 404 si el salario no se encuentra.
            }

            await _dbRepository.DeleteAsync(salario);
            await _dbRepository.SaveChangesAsync();

            return Ok();
        }
    }
}