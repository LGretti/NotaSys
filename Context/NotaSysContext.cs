using Microsoft.EntityFrameworkCore;
using NotaSys.Models;

namespace NotaSys.Context {
    public class NotaSysContext : DbContext {
        public NotaSysContext(DbContextOptions<NotaSysContext> options) : base(options) { }

        public DbSet<Arquivo> Arquivos { get; set; }
    }
}