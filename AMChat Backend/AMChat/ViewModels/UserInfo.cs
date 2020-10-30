using AMChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colma2.ViewModels
{
    public class UserInfo : Usuario
    {
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
