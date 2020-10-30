using System;
using System.Collections.Generic;
using System.Text;
using AMChat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMChat.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}
