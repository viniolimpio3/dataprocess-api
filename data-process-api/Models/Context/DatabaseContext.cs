using Microsoft.EntityFrameworkCore;

namespace data_process_api.Models.Context {
    public class DatabaseContext : DbContext{
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<Funcionario>().HasKey(p => p.Id);

            modelBuilder.Entity<Carro>().ToTable("Carros");
            modelBuilder.Entity<Carro>().HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);

        }
    }
}
