using AuthUserAPI.Models;
using AuthUserAPI.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            var users = await bazis.Users.ToListAsync();
            if (users.Count == 0)
            {
                return StatusCode(404, new
                {
                    message = "Nincs felhasználó az adatbázisban!"
                });
            }
            else
            {
                return StatusCode(200, new
                {
                    message = "Sikeres lekérdezés!",
                    data = users
                });
            }
        }

        [HttpGet("ById")]
        public async Task<IActionResult> ById([FromQuery] Guid id)
        {
            var user = await bazis.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen felhasználó!"
                });
            }
            else
            {
                return StatusCode(200, new
                {
                    message = "Sikeres lekérdezés!",
                    data = user
                });
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserDTO updateUserDTO)
        {
            var user = await bazis.Users.FirstOrDefaultAsync(u => u.Id == updateUserDTO.Id);
            if (user == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen felhasználó!"
                });
            }
            else
            {
                user.Name = updateUserDTO.Name ?? user.Name;
                user.Email = updateUserDTO.Email ?? user.Email;
                user.Password = updateUserDTO.Password ?? user.Password;

                bazis.Users.Update(user);
                await bazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres frissítés!",
                    data = user
                });
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
        {
            var user = await bazis.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return StatusCode(404, new
                {
                    message = "Nincs ilyen felhasználó!"
                });
            }

            else
            {
                bazis.Users.Remove(user);
                await bazis.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "Sikeres törlés!",
                    data = user
                });
            }
        }
    }
}


