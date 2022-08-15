using Microsoft.EntityFrameworkCore;
using SignalRTest.Models;

namespace SignalRTest.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}