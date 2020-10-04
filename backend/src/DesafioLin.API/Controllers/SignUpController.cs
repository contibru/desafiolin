using DesafioLin.API.Security;
using DesafioLin.Domain.Entities;
using DesafioLin.DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DesafioLin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : Controller
    {
        private readonly IUserService _userService;

        public SignUpController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<string> SignUp([FromBody] SignUpDTO signUpDto)
        {
            // Adiciona as autorizações manualmente variando as permissões randomicamente.
            Random r = new Random();

            var authCanAddCustomer = new Authorization()
            {
                Name = "CanAddCustomer",
                Value = r.NextDouble() < 50 / 100.0
            };

            var authCanGetCustomers = new Authorization()
            {
                Name = "CanGetCustomers",
                Value = r.NextDouble() < 50 / 100.0
            };

            var authCanAddProduct = new Authorization()
            {
                Name = "CanAddProduct",
                Value = r.NextDouble() < 50 / 100.0
            };


            var authCanGetProducts = new Authorization()
            {
                Name = "CanGetProducts",
                Value = r.NextDouble() < 50 / 100.0
            };

            signUpDto.authorizations = new List<Authorization>
            {
                authCanAddCustomer,
                authCanGetCustomers,
                authCanAddProduct,
                authCanGetProducts
            };

            var user = new User()
            {
                Login = signUpDto.login,
                Password = signUpDto.password,
                authorizations = signUpDto.authorizations
            };

            _userService.Insert(user);

            if (user.Id > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}