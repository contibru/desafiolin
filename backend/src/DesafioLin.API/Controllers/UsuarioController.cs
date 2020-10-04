using DesafioLin.DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioLin.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET api/values
        [HttpGet("{codigoUsu}/checkauthorization/{authName}")]
        public ActionResult<bool> Get([FromRoute] int codigoUsu, [FromRoute] string authName)
        {
            if (_usuarioService.UsuarioHasAuthorization(codigoUsu, authName))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(_usuarioService.GetAllWithAuthorizations());
        }
    }
}