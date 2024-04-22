using TesteMoot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace TesteMoot.DataAcess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
               .HasMany(u => u.Enderecos);

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nome = "Cliente 1", Email = "teste@email.com.br", ImageUrl = "" },
                new Cliente { Id = 2, Nome = "Cliente 2", Email = "teste@email.com.br", ImageUrl = "" },
                new Cliente { Id = 3, Nome = "Cliente 3", Email = "teste@email.com.br", ImageUrl = "" }
                );

            modelBuilder.Entity<Endereco>().HasData(
                new Endereco
                {
                    Id = 1,
                    Logradouro = "Rua teste 1",
                    Numero = 1000,
                    Complemento = "Apto 301",
                    Bairro = "Bairro teste 1",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Pais = "Brasil",
                    ClienteId = 1
                },
                 new Endereco
                 {
                     Id = 2,
                     Logradouro = "Rua teste 2",
                     Numero = 1000,
                     Complemento = "Apto 401",
                     Bairro = "Bairro teste 2",
                     Cidade = "Belo Horizonte",
                     Estado = "MG",
                     Pais = "Brasil",
                     ClienteId = 1
                 },
                  new Endereco
                  {
                      Id = 3,
                      Logradouro = "Rua teste 1",
                      Numero = 1000,
                      Complemento = "Apto 301",
                      Bairro = "Bairro teste 1",
                      Cidade = "São Paulo",
                      Estado = "SP",
                      Pais = "Brasil",
                      ClienteId = 2
                  }
                );
        }
    }
}
