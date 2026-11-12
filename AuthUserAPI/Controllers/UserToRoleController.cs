using AuthUserAPI.Models;
using AuthUserAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var user = await usertorolebazis.Users.FindAsync(userId);
            var role = await usertorolebazis.Roles.FindAsync(roleId);
            if (user == null || role == null)
            {
                return StatusCode(404, new
                {
                    message = "A felhasználó vagy a szerepkör nem található!"
                });
            }
            Role.Id = role.Id;
            await usertorolebazis.SaveChangesAsync();
            return StatusCode(200, new
            {
                message = "Sikeres hozzárendelés!"
            });
        }

    }
}
