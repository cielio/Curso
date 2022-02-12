
using Curso.api.Filters;
using Curso.api.Models;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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
            var usuarioViewModelOutput = new UsuarioViewModelOutput
            {
                Codigo = 1,
                Login = "cielio",
                Email = "cielion@gmail.com"
            };

            var secret = Encoding.ASCII.GetBytes("MzfsT&d9gprP>!9$Es(X!5g@;ef!5sbk:jH\\2.}8ZP'qY#7");
            var symemtricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symemtricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokemHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokemHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokemHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput.Login
            }); 
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
