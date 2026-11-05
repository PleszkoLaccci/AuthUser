using AuthUserAPI.Models;
using AuthUserAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthUserContext rolesbazis;
        public RolesController(AuthUserContext rolesbazis)
        {
             this.rolesbazis = rolesbazis;
        }

        [HttpPost("NewRole")]
        public async Task<IActionResult> NewRole([FromBody] AddNewRoleDTO roleDto)
        {
            if (roleDto.Name == null)
            {
                return StatusCode(404, new
                {
                    message = "Kérem töltse ki az összes mezőt!"
                });
            }
            else
            {
                var newrole = new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = roleDto.Name,
                };
                await rolesbazis.Roles.AddAsync(newrole);
                await rolesbazis.SaveChangesAsync();
                return StatusCode(201, new
                {
                    message = "Sikeres regisztráció!"
                });
            }
        }

        [HttpGet("AllRoles")]
        public async Task<IActionResult> AllRoles()
        {
            var roles = await rolesbazis.Roles.ToListAsync();
            if (roles.Count == 0)
            {
                return StatusCode(404, new
                {
                    message = "Nincs szerepkör az adatbázisban!"
                });
            }
            else
            {
                return StatusCode(200, new
                {
                    message = "Sikeres lekérdezés!",
                    data = roles
                });
            }
        }

        [HttpGet("ById")]
        public async Task<IActionResult> ById([FromQuery] Guid id)
        {
            var role = await rolesbazis.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen szerepkör az adatbázisban!"
                });
            }
            else
            {
                return StatusCode(200, new
                {
                    message = "Sikeres lekérdezés!",
                    data = role
                });
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromQuery] Guid id, [FromBody] UpdateRoleDTO roleDto)
        {
            var role = await rolesbazis.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen szerepkör az adatbázisban!"
                });
            }
            else
            {
                role.Name = roleDto.Name;
                await rolesbazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres módosítás!"
                });
            }
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] Guid id)
        {
            var role = await rolesbazis.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen szerepkör az adatbázisban!"
                });
            }
            else
            {
                rolesbazis.Roles.Remove(role);
                await rolesbazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres törlés!"
                });
            }
        }

    }
}
