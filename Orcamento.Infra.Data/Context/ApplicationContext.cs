using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }

        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Produto);

            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Vendedor);

        }
    }
}
