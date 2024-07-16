using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace PasswordManagerTask.Data
{
    public class PasswordContext : DbContext
    {
        public PasswordContext(DbContextOptions<PasswordContext> options) : base(options) { }
        public DbSet<PasswordEntity> Password { get; set; } 
    }
}
