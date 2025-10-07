using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using authbackend.Entities;
using authBackEnd.Extensions;
using Newtonsoft.Json;
using authbackend.Dtos;
using authbackend.Interface;

namespace authbackend.Controllers
{

    [ApiController]
    [Route("horastrabajadas")]
    public class HorasTrabajadasController : ControllerBase{
            private readonly IRepository<HorasTrabajadas, int> _dbRepository;

            
            public HorasTrabajadasController(IRepository<HorasTrabajadas, int> dbRepository ){
                _dbRepository = dbRepository; 
            }   
            
            [HttpGet("table")]
            public async Task<ActionResult<List<TablaHorasTrabajadasDto>>> GetHorasTrabajadasTable(){
                var horasTrabajadasDto = await _dbRepository.GetQueryable()
                .Include(h => h.User)
                .Select(h => new TablaHorasTrabajadasDto {
                    Id = h.Id,
                    Names = h.User != null ? h.User.Names : "",
                    LastName = h.User != null ? h.User.LastName : "",
                    SecondLastName = h.User != null ? h.User.SecondLastName : "",
                    Horas = h.Horas,
                    Fecha = h.Fecha
                }).ToListAsync();

                return Ok(horasTrabajadasDto);
            }

            
            [HttpPut("table/update/{Id}")]
            public async Task<IActionResult> UpdateHorasTrabajadasTable(int Id, [FromBody] string jsonValues)
            {
                var horasTrabajadas =await _dbRepository.GetByIdAsync(Id);
            
                if (horasTrabajadas == null)
                    return NotFound();

                JsonConvert.PopulateObject(jsonValues, horasTrabajadas);
                
                if (!TryValidateModel(horasTrabajadas))
                    return BadRequest(ModelState.GetFullErrorMessage());

                await _dbRepository.SaveChangesAsync();

                return Ok(horasTrabajadas);
            }

            [HttpPost("table/create")]
            public async Task<ActionResult> CreateHorasTrabajadasTable(HorasTrabajadas newHoras){
                bool mes = await _dbRepository.GetQueryable().AnyAsync(u => u.Fecha == newHoras.Fecha  && u.User == newHoras.User);

                if (mes){
                    return BadRequest("Mes ya esta registrado.");
                }
                await _dbRepository.InsertAsync(newHoras);
                await _dbRepository.SaveChangesAsync();  
                return Ok();     
            }

            [HttpDelete("table/delete/{id}")]
            public async Task<ActionResult> DeleteHorasTrabajadasTable(int id){
                var horas = await _dbRepository.GetByIdAsync(id);
            
                if (horas == null){
                    return NotFound(); // Devuelve 404 si el usuario no se encuentra.
                }

                await _dbRepository.DeleteAsync(horas); // Elimina el mes.
                await _dbRepository.SaveChangesAsync(); // Guarda los cambios en la base de datos.
                return Ok(); 
            }
    }
}
