using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using infraestructure.Model;

namespace infraestructure.Repository
{
    public class UsuarioRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;

        public UsuarioRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public string insertarUsuario(UsuarioModel usuario)
        {
            try
            {
                connection.Execute("insert into usuarios( id , id_persona , usuario , password , estado  ) " +
                "values (@Id ,@id_persona,  @Usuario , @Password, @Estado)",
                usuario);
                return "Se inserto Correctamente";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string modificarUsuario(UsuarioModel usuario, string usuario_)
        {
            try
            {
                _ = connection.Execute($" UPDATE usuarios set " +
                    "id = @Id , " +
                    "id_persona = @id_persona , " +
                    "usuario = @Usuario , " +
                    "password  = @Password , " +
                    "estado = @Estado  " +
                    $"where usuario = '{usuario_}'", usuario);

                return "Se modificaron los datos correctamente";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string eliminarUsuario(int id)
        {
            try
            {
                connection.Execute($" DELETE from usuarios " +
                    $"where id = {id}");
                return "Se elimino correctamente";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public UsuarioModel consultarUsuario(string usuario)
        {
            try
            {
                return connection.QueryFirst<UsuarioModel>($" select * from usuarios where usuario = '{usuario}'");
            }
            catch (Exception ex)
            {
                throw new Exception("No existe registro  con el usuario ingresado!");
            }

        }

        public IEnumerable<UsuarioModel> listarUsuario()
        {
            try
            {
                return connection.Query<UsuarioModel>(" select * from usuarios order by id asc ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
