using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Colma2.ViewModels
{
    public class UserView
    {
        public string UserID { get; set; }

        public string RoleID { get; set; }

        public string Name { get; set; }
        public string Foto { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public RoleView role { get; set; }
        public List<RoleView> roles { get; set; }
    }
}
