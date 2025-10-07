using authbackend.Dtos;
using authbackend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using authbackend.Entities;
using authBackEnd.Extensions;
using Newtonsoft.Json;

namespace authbackend.Controllers
{
    [ApiController]
    [Route("nomina")]
    public class NominaController : ControllerBase
    {
        private readonly IRepository<Nomina, int> _dbRepository;

        public NominaController(IRepository<Nomina, int> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet("chart")]
        public async Task<ActionResult<IEnumerable<NominaChartDto>>> GetNominaChart(){
            var tablaBasica = await _dbRepository.GetQueryable()
                .Select(r => new NominaChartDto
                {
                    Mes = r.Mes,
                    Cantidad = r.Cantidad
                })
                .ToListAsync();

            return Ok(tablaBasica);
        }

        [HttpGet("table")]
        public async Task<ActionResult<IEnumerable<Nomina>>> GetNominaTable()
        {
            var nomina = await _dbRepository.GetAllAsync();
            return Ok(nomina);
        }

        [HttpPut("table/update/{Id}")]
        public async Task<IActionResult> UpdateNominaTable(int Id, [FromBody] string jsonValues)
        {
            var nomina = await _dbRepository.GetByIdAsync(Id);

            if (nomina == null)
                return NotFound();

            JsonConvert.PopulateObject(jsonValues, nomina);

            if (!TryValidateModel(nomina))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _dbRepository.UpdateAsync(nomina);
            await _dbRepository.SaveChangesAsync();

            return Ok(nomina);
        }

        [HttpPost("table/create")]
        public async Task<ActionResult> CreateNominaTable(Nomina newNomina)
        {
            bool mes = await _dbRepository.GetQueryable().AnyAsync(u => u.Mes == newNomina.Mes);

            if (mes)
            {
                return BadRequest("Mes ya está registrado.");
            }

            await _dbRepository.InsertAsync(newNomina);
            await _dbRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("table/delete/{id}")]
        public async Task<ActionResult> DeleteNominaTable(int id)
        {
            var nomina = await _dbRepository.GetByIdAsync(id);

            if (nomina == null)
            {
                return NotFound(); // Devuelve 404 si la nómina no se encuentra.
            }

            await _dbRepository.DeleteAsync(nomina);
            await _dbRepository.SaveChangesAsync(); // Guarda los cambios en la base de datos.
            return Ok();
        }
    }
}
