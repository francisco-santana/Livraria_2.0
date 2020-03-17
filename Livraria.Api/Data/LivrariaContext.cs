using Livraria.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Api.Data
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions options) : base(options)
        {
        }

        public LivrariaContext()
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
