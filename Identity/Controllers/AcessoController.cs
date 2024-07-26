using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "IdadeMinima")]
        public IActionResult Get() {
            return Ok("Acesso permitido!");

        }
    }
}
