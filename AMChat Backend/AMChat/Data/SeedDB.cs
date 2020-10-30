using AMChat.Data;
using AMChat.Helpers;
using AMChat.Models;
using Colma2.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colma2.Data
{
    public class SeedDB
    {

        private readonly ApplicationDbContext _context;
        private readonly IUserHelper userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedDB(
            ApplicationDbContext dbContext,
            IUserHelper userHelper,
            RoleManager<IdentityRole> RoleManager)
        {
            _context = dbContext;
            this.userHelper = userHelper;
            _roleManager = RoleManager;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CreateUsers();
        }

        private async Task CreateUsers()
        {
            if (!_context.Users.Any())
            {
                var mauricio = new Usuario
                {
                    Apellidos = "Perez",
                    Email = "mauricio@amchat.com",
                    Nombres = "Mauricio",
                    UserName = "mauricio@amchat.com",
                };

                var Ana = new Usuario
                {
                    Apellidos = "Caran",
                    Email = "Ana@amchat.com",
                    Nombres = "Ana",
                    UserName = "Ana@amchat.com",
                };

                var r1 = await userHelper.CreateUserAsync(mauricio, "Abcd1234.");
                var r2 = await userHelper.CreateUserAsync(Ana, "Abcd1234.");

                if (r1.IsSuccess && r2.IsSuccess)
                {
                    var user1 = r1.Result as Usuario;
                    var user2 = r2.Result as Usuario;
                    var chat = new ChatRoom
                    {
                        User1Id = user1.Id,
                        User2Id = user2.Id,                        
                    };

                    await _context.AddAsync(chat);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
