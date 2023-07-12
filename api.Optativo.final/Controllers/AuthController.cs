using api.cuentas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace api.Optativo.final.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private UsuarioService usuarioService;

        private IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.usuarioService = new UsuarioService(configuration.GetConnectionString("postgresDB"));

        }

        readonly byte[] key = Encoding.ASCII.GetBytes("lkj@jalsd.123@aadll.hhd33esa!qwertyuiop123");

        [HttpPost]
        public IActionResult Autenticar([FromBody]  LoginModel loginModel)
        {
            if(!usuarioAutenticado(loginModel.Username, loginModel.Password))  return Unauthorized();
            var token = crearToken(loginModel.Username);
            return Ok(token);
        }

        private bool usuarioAutenticado(string user, string password)
        {
            
            var usuarioModel = usuarioService.consultarUsuario(user);
            var usuario = usuarioModel.Usuario;
            var contrasena = usuarioModel.Password;

            return user == usuario && password == contrasena;
        }

        private string crearToken(string user)
        {
            var handlerToken = new JwtSecurityTokenHandler();
            var descriptorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user),
                }),
                Expires
                = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)

            };

                var token = handlerToken.CreateToken(descriptorToken);

            return handlerToken.WriteToken(token);
        }
    }
        public class LoginModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password{ get; set; }
        }
    
}
