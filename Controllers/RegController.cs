using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldOfPets.Models;

namespace WorldOfPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegController : ControllerBase
    {
        /// <summary>
        /// Инициализируем подключение к БД
        /// </summary>
        UsersContext db;

        /// <summary>
        /// Присваеваем контекст подключения для дальнейшего использования
        /// </summary>
        /// <param name="context">Принимает контекст подключения</param>
        public RegController(UsersContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegUsers([Required(ErrorMessage = "Не указан логин")]
                                                  string Login
                                                , [Required(ErrorMessage = "Не указана фамилия")]
                                                  string LastName
                                                , [Required(ErrorMessage = "Не указано имя")]
                                                  string FirstName
                                                , [Required(ErrorMessage = "Не указан пароль")]
                                                  [DataType(DataType.Password)]
                                                  string Pass
                                                , [Required(ErrorMessage = "Не указан пол")]
                                                  string Gender
                                                , [Required(ErrorMessage = "Не указан email")]
                                                  string Email
                                                , [Required(ErrorMessage = "Не указана дата рождения")]
                                                  string DateOfBirth
                                                , [Required(ErrorMessage = "Не указан часовой пояс")]
                                                  string Timezone
                                                , [Required(ErrorMessage = "Не указана Страна")]
                                                  string Country
                                                , [Required(ErrorMessage = "Не указан город")]
                                                  string City
                                                , [Required(ErrorMessage = "Не указан адрес")]
                                                  string Address
                                                , Byte Status = 1
                                                , string Patronymic = ""
                                                , Int64 Phone = 0)
        {
            var param = new Microsoft.Data.SqlClient.SqlParameter[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Login",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 30,
                        Value = Login
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Status",
                        SqlDbType = System.Data.SqlDbType.TinyInt,
                        Value = Status
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_LastName",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 32,
                        Value = LastName
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_FirstName",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 32,
                        Value = FirstName
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Patronymic",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 32,
                        Value = Patronymic
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Pass",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 16,
                        Value = Pass
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Gender",
                        SqlDbType = System.Data.SqlDbType.NChar,
                        Size = 4,
                        Value = Gender
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Email",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 128,
                        Value = Email
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Phone",
                        SqlDbType = System.Data.SqlDbType.BigInt,
                        Value = Phone
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_DateOfBirth",
                        SqlDbType = System.Data.SqlDbType.Date,
                        Value = DateOfBirth
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_DateOfCreation",
                        SqlDbType = System.Data.SqlDbType.DateTime2,
                        Value = DateTime.Now
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_DateOfChange",
                        SqlDbType = System.Data.SqlDbType.DateTime2,
                        Value = DateTime.Now
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Timezone",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 60,
                        Value = Timezone
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Country",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 60,
                        Value = Country
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_City",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 60,
                        Value = City
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_Address",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 512,
                        Value = Address
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "@_result",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Direction = System.Data.ParameterDirection.Output,
                        Size = 256
                    }
                };

            List<InfoReg> infoReg = await db.Info_Reg.FromSqlRaw(@$"[dbo].[Set_AddUser] @Login          = @_Login
                                                                                       ,@Status         = @_Status
                                                                                       ,@LastName       = @_LastName
                                                                                       ,@FirstName      = @_FirstName
                                                                                       ,@Patronymic     = @_Patronymic
                                                                                       ,@PASS           = @_Pass
                                                                                       ,@Gender         = @_Gender
                                                                                       ,@Email          = @_Email
                                                                                       ,@Phone          = @_Phone
                                                                                       ,@DateOfBirth    = @_DateOfBirth
                                                                                       ,@DateOfCreation = @_DateOfCreation
                                                                                       ,@DateOfChange   = @_DateOfChange
                                                                                       ,@Timezone       = @_Timezone
                                                                                       ,@Country        = @_Country
                                                                                       ,@City           = @_City
                                                                                       ,@Address        = @_Address
                                                                                       ,@result         = @_result output;'", param).ToListAsync();

            if (infoReg.Count >= 0)
            {                
                //await Authenticate(infoAuth[0]); // аутентификация

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return Redirect($"auth?Login={Login}&Pass={Pass}");
                }
            }

            // если пользователя не найдено
            return NotFound();
        }
    }
}
