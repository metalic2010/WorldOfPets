using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldOfPets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WorldOfPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        UsersContext db;
        public UsersController(UsersContext context)
        {
            db = context;
        }

        // GET api/users/5
        [HttpGet] //("{login}")
        public async Task<ActionResult<InfoUsers>> Get(string login)
        {
            InfoUsers user = await db.Info_Users.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper());
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        /// <summary>
        /// Получение информации о друзьях
        /// api/users/friend
        /// </summary>
        /// <param name="login">
        /// Принимает логин авторизованного пользователя
        /// </param>
        /// <returns>Возвращает список друзей</returns>
        [HttpGet("friend")]
        public async Task<ActionResult<InfoUsers>> GetFriend()
        {
            InfoUsers user = await db.Info_Users.FirstOrDefaultAsync(x => x.Login.ToUpper() == User.Identity.Name.ToUpper());
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<InfoUsers>> Post(InfoUsers user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Info_Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<InfoUsers>> Put(InfoUsers user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Info_Users.Any(x => x.Login == user.Login))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{Login}")]
        public async Task<ActionResult<InfoUsers>> Delete(string Login)
        {
            InfoUsers user = db.Info_Users.FirstOrDefault(x => x.Login == Login);
            if (user == null)
            {
                return NotFound();
            }
            db.Info_Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

    }
}