﻿using DesafioLin.Domain.Entities;
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
                return Forbid();
            }
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(_userService.GetAllWithAuthorizations());
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody] User user)
        {
            _userService.Update(user);

            return Ok();
        }
    }
}