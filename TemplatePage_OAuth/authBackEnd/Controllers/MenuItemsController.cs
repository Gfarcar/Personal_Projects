using Microsoft.AspNetCore.Mvc;
using authbackend.Entities;
using authbackend.Interface;
using authbackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Controllers
{

    [ApiController]
    [Route("menuitems")]
    public class MenuItemsController : ControllerBase{
            private readonly IRepository<MenuItem, int> _dbRepository;

            
            public MenuItemsController(IRepository<MenuItem, int> dbRepository ){
                _dbRepository = dbRepository; 
            }   
            
    

            [HttpGet("{id}")]
            public async Task<ActionResult<List<MenuItemDto>>> GetMenuItemsByRoleId(int id)
            {
                // Obtén los items del menú filtrados por RoleId (asumiendo que tienes un repositorio que lo maneja)
                var menuItems = await _dbRepository.GetQueryable()
                .Include(mp => mp.MenuPermissions)
                    .Where(mp => mp.MenuPermissions.Any(perm => perm.RoleId == id && perm.CanRender)) // Filtra usando Any para la colección de permiso
                    .Select(mp => new MenuItemDto
                    {
                        Id = mp.Id.ToString(),
                        Title = mp.Title,
                       // Icon = mp.Icon,
                        Href = mp.Href
                    }).ToListAsync();
                
                // Verifica si se encontraron resultados
                if (menuItems == null || !menuItems.Any())
                {
                    return NotFound($"No menu items found for role with ID = {id}");
                }

                // Devuelve la lista de items del menú
                return Ok(menuItems);
            }
        
    }
}
