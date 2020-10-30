using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMChat.Data;
using AMChat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMChat.API
{
    [Route("api/[controller]")]
    [Route("api/Chat")]
    public class ChatRoomController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ChatRoomController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("getChat")]
        [HttpGet]
        public async Task<IActionResult> ChatRoom(string id)
        {
            var chatRoom = await dbContext.ChatRooms
                .Include(a => a.User1)
                .Include(a => a.User2)
                .FirstOrDefaultAsync(a=> a.User1Id == id || a.User2Id == id);

            chatRoom.Mensajes = dbContext.Mensajes.Where(a => a.ChatRoomIdChatRoom == chatRoom.IdChatRoom).Select(a=> new Mensaje { Message = a.Message, ChatRoomIdChatRoom = a.ChatRoomIdChatRoom, EmisorId = a.EmisorId, Fecha = a.Fecha, IdMensaje = a.IdMensaje, Leido = a.Leido, TipoMensaje = a.TipoMensaje, Emisor = a.Emisor}).ToList();

            if (chatRoom.User2Id == id)
            {
                return Ok(new { chatId = chatRoom.IdChatRoom, userId = chatRoom.User1Id, user = chatRoom.User1.FullName, mensajes = chatRoom.Mensajes });
            }
            else
            {
                return Ok(new { chatId = chatRoom.IdChatRoom, userId = chatRoom.User2Id, user = chatRoom.User2.FullName, mensajes = chatRoom.Mensajes });
            }
        }
    }
}
