using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Models
{
    public class ChatRoom
    {
        [Key]
        public int IdChatRoom { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual Usuario User1 { get; set; }
        public virtual Usuario User2 { get; set; }
    }
}
