using DesafioLin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DesafioLin.Infraestructure.Context
{
    public class DesafioLinContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public DesafioLinContext(DbContextOptions options, IConfiguration configuration)
          : base(options)
        {
            Configuration = configuration;
        }

        public DesafioLinContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                //optionsBuilder.UseSqlite(Configuration["ConexaoSqlite:SqliteConnectionString"]);
                optionsBuilder.UseSqlite("Data Source=..\\DesafioLin.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Esse comando aplica os mapeamentos (Configurations no EF Core) para todas as classes.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioLinContext).Assembly);

            modelBuilder.Entity<Usuario>();

            modelBuilder.Entity<Authorization>();
        }
    }
}