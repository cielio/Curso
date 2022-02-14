using cursos.api.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace curso.api.Configurations
{
    public class JwtService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput)
        {
 

            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
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

            return token;
        }
    }
}
