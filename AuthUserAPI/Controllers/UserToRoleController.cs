using AuthUserAPI.Models;
using AuthUserAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserToRoleController : ControllerBase
    {
        private readonly AuthUserContext usertorolebazis;

        public UserToRoleController(AuthUserContext usertorolebazis)
        {
            this.usertorolebazis = usertorolebazis;
        }

        [HttpPut("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser([FromQuery] Guid userId, [FromQuery] Guid roleId, AddRolesToUserDTO addRolesToUserDTO)
        {
            try
            {
              var user = await usertorolebazis.Users.Include(u => u.Roles)
              .SingleOrDefaultAsync(u => u.Id == addRolesToUserDTO.UserId);
                var roles = await usertorolebazis.Roles.FindAsync(addRolesToUserDTO.RolesId);

                if (user == null || roles == null)
                {
                    return NotFound(new
                    {
                        message = "A felhasználó vagy a szerepkör nem található!"
                    });
                }

                user.Roles.Add(roles);
                await usertorolebazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres hozzárendelés!"
                });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new
                {
                    message = "Hiba történt a hozzárendelés során!",
                    error = exception.Message
                });
            }
        }

        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] AddRolesToUserDTO addRolesToUserDTO)
        {
            try
            {
                var user = await usertorolebazis.Users.Include(u => u.Roles)
              .SingleOrDefaultAsync(u => u.Id == addRolesToUserDTO.UserId);
                var roles = await usertorolebazis.Roles.FindAsync(addRolesToUserDTO.RolesId);
                if (user == null || roles == null)
                {
                    return StatusCode(404, new
                    {
                        message = "A felhasználó evvel a szerepkörrel nem található!"
                    });
                }
                user.Roles.Remove(roles);
                await usertorolebazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres eltávolítás!"
                });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new
                {
                    message = "Hiba történt a hozzárendelés során!",
                    error = exception.Message
                });
            }
        }
    }
}
