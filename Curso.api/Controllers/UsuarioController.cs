
using curso.api.Business.Repositories;
using curso.api.Configurations;
using cursos.api.Business.Entities;
using cursos.api.Filters;
using cursos.api.Infrastruture.Data;
using cursos.api.Models;
using cursos.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace cursos.api.Controllers
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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, 
            IAuthenticationService authenticationService, 
            ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost]
        [Route("logar")]
        [ValidaçaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            try
            {
                var usuario =  _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

                if (usuario == null)
                {
                    return BadRequest("Houve um erro ao tentar acessar.");
                }

                //if (usuario.Senha != loginViewModel.Senha.GerarSenhaCriptografada())
                //{
                //    return BadRequest("Houve um erro ao tentar acessar.");
                //}

                var usuarioViewModelOutput = new UsuarioViewModelOutput()
                {
                    Codigo = usuario.Codigo,
                    Login = loginViewModelInput.Login,
                    Email = usuario.Email
                };

                var token = _authenticationService.GerarToken(usuarioViewModelOutput);

                return Ok(new LoginViewModelOutput
                {
                    Token = token,
                    Usuario = usuarioViewModelOutput
                });
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("registrar")]
        [ValidaçaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {

            var usuario = new Usuario();
            usuario.Login = registroViewModelInput.Login;
            usuario.Senha = registroViewModelInput.Senha;
            usuario.Email = registroViewModelInput.Email;
            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();
            return Created("",registroViewModelInput);
        }
    }
}
