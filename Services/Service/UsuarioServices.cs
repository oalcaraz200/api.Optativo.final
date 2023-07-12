
using infraestructure.Model;
using infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsuarioService
    {
        private UsuarioRepository usuarioRepository;
        public UsuarioService(String connectionString)
        {
            this.usuarioRepository = new UsuarioRepository(connectionString);

        }

        public string insertarUsuario(UsuarioModel usuario)
        {
            return validadDatosUsuario(usuario) ? usuarioRepository.insertarUsuario(usuario) : throw new Exception("Error en la validacion");
        }


        public string modificarUsuario(UsuarioModel usuario, string usuario_)
        {
            return validadDatosUsuario(usuario) ? usuarioRepository.modificarUsuario(usuario, usuario_) : throw new Exception("Error en la validacion");
        }


        public string eliminarUsuario(int id)
        {

            return usuarioRepository.eliminarUsuario(id);
        }

        public IEnumerable<UsuarioModel> listarUsuarios()
        {
            return usuarioRepository.listarUsuario();
        }

        public UsuarioModel consultarUsuario(string usuario)
        {
            return usuarioRepository.consultarUsuario(usuario);
        }


        private bool validadDatosUsuario(UsuarioModel usuario)
        {
            // if(persona.Nombre.Trim().Length < 2)
            //    {
            //        return false;
            //     }
            return true;
        }
    }
}