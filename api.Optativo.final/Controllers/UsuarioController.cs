using api.cuentas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Service;
using Services.Services;

namespace api.Optativo.final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioService usuarioService;

        private IConfiguration configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.usuarioService = new UsuarioService(configuration.GetConnectionString("postgresDB"));

        }
        
        [HttpGet("ListarUsuario")]
        public ActionResult<List<UsuarioModel>> ListarUsuario()
        {
            var resultado = usuarioService.listarUsuarios();
            return Ok(resultado);
        }


        [HttpGet("ConsultarUsuario/{usuario}")]
        public ActionResult<UsuarioModel> ConsultarUsuario(string  usuario)
        {
            var resultado = usuarioService.consultarUsuario(usuario);
            return Ok(resultado);
        }

        [HttpPost("InsertarUsuario")]
        public ActionResult<string> insertarUsuario(UsuarioModel modelo)
        {
            var resultado = this.usuarioService.insertarUsuario(new infraestructure.Model.UsuarioModel
            {
                Id = modelo.Id ,
                id_persona = modelo.id_persona,
                Usuario = modelo.Usuario ,
                Password = modelo.Password ,
                Estado = modelo.Password 
              });
            
            return Ok(resultado);
        }

        [HttpPut("ModificarUsuario/{usuario}")]
        public ActionResult<string> modificarUsuario(UsuarioModel modelo , string usuario )
        {
            if (usuarioService.consultarUsuario(usuario) != null)
            {
                var resultado = this.usuarioService.modificarUsuario(new infraestructure.Model.UsuarioModel
                {
                    id_persona = modelo.id_persona,
                    Usuario = modelo.Usuario,
                    Password = modelo.Password,
                    Estado = modelo.Estado
                }, usuario);

                return Ok(resultado);
            }
            else
                return NotFound("Usuario no Existe!");
           
        }

        [HttpDelete("EliminarUsuario/{id}")]
        public ActionResult<string> eliminarUsuario( int id)
        {
            var resultado = this.usuarioService.eliminarUsuario(id);
            return Ok(resultado);
        }

    }
}
