using HenriqueSantos.Torneio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HenriqueSantos.Torneio.API.Data
{
    public class TorneioContext : DbContext
    {
        public TorneioContext(DbContextOptions<TorneioContext> options) : base(options) { }

        public DbSet<Campeonato> Campeonatos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Campeonato>().Property(c => c.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Entity<Campeonato>().Property(c => c.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Entity<Campeonato>().ToTable("Campeonatos");
        }

    }
}
