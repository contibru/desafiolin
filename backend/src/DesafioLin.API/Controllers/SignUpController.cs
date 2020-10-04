using DesafioLin.API.Security;
using DesafioLin.Domain.Entities;
using DesafioLin.DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioLin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public SignUpController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult<string> SignUp([FromBody] SignUpDTO signUpDto)
        {
            var authCanAddCustomer = new Authorization()
            {
                Name = "CanCanAddCustomer",
                Value = true
            };

            var authCanAddProduct = new Authorization()
            {
                Name = "CanAddProduct",
                Value = true
            };

            var authCanGetCustomers = new Authorization()
            {
                Name = "CanGetCustomers",
                Value = true
            };

            var authCanGetProducts = new Authorization()
            {
                Name = "CanGetProducts",
                Value = true
            };

            signUpDto.authorizations = new List<Authorization>
            {
                authCanAddCustomer,
                authCanGetCustomers,
                authCanAddProduct,
                authCanGetProducts
            };

            var usuario = new Usuario()
            {
                Login = signUpDto.login,
                Senha = signUpDto.senha,
                authorizations = signUpDto.authorizations
            };

            _usuarioService.Insert(usuario);

            if (usuario.Id > 0)
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