using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Get() {

            if (User.IsInRole("Admin"))
            {
                // ... Lógica específica para administradores
            }
            else
            {
                // ... Lógica para usuários comuns
            }

            return Ok("Acesso permitido!");

        }

        [HttpGet]
        [Route("AcessoUser")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public IActionResult AcessoUser()
        {
            if (User.IsInRole("Admin"))
            {
                // ... Lógica específica para administradores
            }
            else
            {
                // ... Lógica para usuários comuns
            }

            return Ok("Acesso permitido!");

        }

        [HttpGet]
        [Route("AcessoAdmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public IActionResult AcessoAdmin()
        {
            if (User.IsInRole("Admin"))
            {
                // ... Lógica específica para administradores
            }
            else
            {
                // ... Lógica para usuários comuns
            }

            return Ok("Acesso permitido!");

        }
    }
}
