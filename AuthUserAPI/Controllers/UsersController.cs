using AuthUserAPI.Models;
using AuthUserAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace AuthUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthUserContext bazis;

        public UsersController(AuthUserContext bazis)
        {
            this.bazis = bazis;
        }


        [HttpPost("NewUser")]
        public async Task<IActionResult> NewUser([FromQuery] NewUserDTO newuserDTO)
        {
            if(newuserDTO.Name == null || newuserDTO.Email == null || newuserDTO.Password == null)
            {
                return StatusCode(404, new
                {
                    message = "Kérem töltse ki az összes mezőt!"
                });
            }
            else
            {
                var newuser = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = newuserDTO.Name,
                    Email = newuserDTO.Email,
                    Password = newuserDTO.Password,
                };

                await bazis.Users.AddAsync(newuser);

                await bazis.SaveChangesAsync();

                return StatusCode(201, new
                {
                    message = "Sikeres regisztráció!"
                });
            }
        }
    }
}


