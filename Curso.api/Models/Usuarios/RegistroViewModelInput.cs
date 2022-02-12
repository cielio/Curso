using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Models.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage ="O Login é Obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O E-mail é Obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é Obrigatório")]
        public string Senha { get; set; }
    }
}
