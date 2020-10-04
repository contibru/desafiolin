using DesafioLin.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioLin.API.Controllers
{
    [Route("api/[controller]")]
    public class SignInController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody] AccessCredentials credenciais,
            [FromServices] AccessManager accessManager)
        {
            AccessCredentials credencialAutenticada;

            credencialAutenticada = accessManager.ValidateCredentials(credenciais);

            if (credencialAutenticada != null)
            {
                return accessManager.GenerateToken(credencialAutenticada);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}