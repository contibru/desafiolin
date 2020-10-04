using DesafioLin.Domain.Entities;
using DesafioLin.DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioLin.API.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        [HttpGet("{idUser}/checkauthorization/{authName}")]
        public ActionResult<bool> Get([FromRoute] int idUser, [FromRoute] string authName)
        {
            if (_userService.UserHasAuthorization(idUser, authName))
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
            return Ok(_userService.GetAllWithAuthorizations());
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] User user)
        {
            _userService.Insert(user);

            if (user.Id > 0)
            {
                // Insere o orçamento
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody] User user)
        {
            _userService.Update(user);

            return Ok();
        }
    }
}