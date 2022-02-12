
using Curso.api.Filters;
using Curso.api.Models;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Curso.api.Controllers
{
    /// <summary>
    /// Este serviço permite autenticar um usuario cadastrado e ativo.
    /// </summary>
    /// <param name="LoginViewModelInput">View model do login</param>
    /// <returns>Retorna status ok, dados do usuario e o token em caso</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]

    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("logar")]
        [ValidaçaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
                return Ok(loginViewModelInput); 
        }

        [HttpPost]
        [Route("registrar")]
        [ValidaçaoModelStateCustomizado]
        public IActionResult Logar(RegistroViewModelInput registroViewModelInput)
        {
            return Created("",registroViewModelInput);
        }
    }
}
