using System.ComponentModel.DataAnnotations;

namespace api.cuentas.Models
{
    public class UsuarioModel
    {

        [Required]
        public int Id { get; set; }
       
        [Required]
        [MinLength(3)]
        public string Usuario { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int id_persona { get; set; }

        public string Estado { get; set; }

    }
}
