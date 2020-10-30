using AMChat.Data;
using AMChat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext dbContext;

        public ChatHub(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendMessage(string userId, string message, int idChatRoom)
        {
            var user = dbContext.Users.FirstOrDefault(a => a.Id == userId);

            var mj = new Mensaje
            {
                ChatRoomIdChatRoom = idChatRoom,
                EmisorId = user.Id,
                Fecha = DateTime.Now,
                Message = message,
            };

            dbContext.Mensajes.Add(mj);
            await dbContext.SaveChangesAsync();

            mj.Emisor = user;

            await Clients.All.SendAsync($"{mj.ChatRoomIdChatRoom}", mj);

           await Clients.Group($"").SendAsync($"{mj.ChatRoomIdChatRoom}", user.FullName, dbContext.Mensajes.Find(mj.IdMensaje));


            //NotificationHelper.ExcutePushNotification(user.FullName, message, token.Token, pedido, "pedido");

        }

        public async Task ReadedMessage(int idchatRoom)
        {
            var messages = await dbContext.Mensajes.Where(a => a.ChatRoomIdChatRoom == idchatRoom).ToListAsync();

            foreach (var item in messages)
            {
                item.Leido = true;
            }

            dbContext.UpdateRange(messages);
            await dbContext.SaveChangesAsync();

            await Clients.All.SendAsync($"{idchatRoom}MessagesLeidos");
        }
    }
}
