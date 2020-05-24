using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using WorldOfPets.Models;

namespace WorldOfPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Инициализируем подключение к БД
        /// </summary>
        UsersContext db;

        /// <summary>
        /// Присваеваем контекст подключения для дальнейшего использования
        /// </summary>
        /// <param name="context">Принимает контекст подключения</param>
        public AuthController(UsersContext context)
        {
            db = context;
        }

        /// <summary>
        /// Аутентификация пользователя, создание авторизационной куки
        /// </summary>
        /// <param name="userAuth">
        /// Принимает информацию о пользователе для создания куки
        /// </param>
        /// <returns>Возвращают информацию куки в клиент</returns>
        private async Task Authenticate(InfoAuth userAuth)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userAuth.Login),
                new Claim(ClaimTypes.Name, userAuth.Login),
                new Claim(ClaimTypes.Role, userAuth.RoleName)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims
                                                  , "ApplicationCookie"
                                                  , ClaimsIdentity.DefaultNameClaimType
                                                  , ClaimsIdentity.DefaultRoleClaimType);

            var authProperties = new AuthenticationProperties
            {
                //Обновление сеанса аутентификации
                AllowRefresh = true,

                //Время истечения билета аутентификации
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                //Сохраняется ли сеанс аутентификации в несколько запросов
                IsPersistent = true,

                //Время, когда был выдан билет для аутентификации
                IssuedUtc = DateTimeOffset.UtcNow,

                //RedirectUri = <string>
            };

            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                                         , new ClaimsPrincipal(id)
                                         , authProperties);
        }

        /// <summary>
        /// Принимает данные из формы авторизации и авторизует пользователя
        /// если тот существует в БД и активен (т.е. не удалён и не заблокирован)
        /// </summary>
        /// <param name="Login">Принимает логин</param>
        /// <param name="Pass">Принимает пароль</param>
        /// <returns>Возвращает в клиент ответ от сервера</returns>
        [HttpGet]
        public async Task<IActionResult> Logon([Required(ErrorMessage = "Не указан логин")]
                                               string Login
                                             , [Required(ErrorMessage = "Не указан пароль")]
                                               [DataType(DataType.Password)]
                                               string Pass)
        {
            List<InfoAuth> infoAuth = await db.Info_Auth.FromSqlRaw(@$"[dbo].[Get_CheckPassword] @Login = '{Login}', @PASS = '{Pass}'").ToListAsync();

            if (infoAuth.Count > 0)
            {
                await Authenticate(infoAuth[0]); // аутентификация
                
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return Redirect($"users?Login={User.Identity.Name}");
                }
            }

            // если пользователя не найдено
            return NotFound();
        }

        /// <summary>
        /// API выход из сессии
        /// Пример: api/auth/logout
        /// </summary>
        /// <returns>
        /// Перенаправляет на страницу входа (Login/Account)
        /// </returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}