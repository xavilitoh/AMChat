using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Models
{
    public class Usuario : IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string FotoUrl { get; set; }
        public string FullName { get { return $"{this.Nombres} {this.Apellidos}"; } }
    }
}