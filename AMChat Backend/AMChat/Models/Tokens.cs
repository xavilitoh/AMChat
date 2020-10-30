using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Models
{
    public class Tokens
    {
        [Key]
        public int IdTokens { get; set; }

        public string UsuarioId { get; set; }

        public string Token { get; set; }

        public string Tipo { get; set; }

        public DateTime UltimoUso { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
