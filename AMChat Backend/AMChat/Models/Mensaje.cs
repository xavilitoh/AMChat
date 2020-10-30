using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Models
{
    public class Mensaje
    {
        [Key]
        public int IdMensaje { get; set; }

        [Display(Name = "Chat Room")]
        public int ChatRoomIdChatRoom { get; set; }

        public string Message { get; set; }

        public DateTime Fecha { get; set; }

        public string EmisorId { get; set; }

        public bool Leido { get; set; }
        public TipoMensaje TipoMensaje { get; set; }
        public virtual ChatRoom ChatRoom { get; set; }
        public virtual Usuario Emisor { get; set; }
    }
}
