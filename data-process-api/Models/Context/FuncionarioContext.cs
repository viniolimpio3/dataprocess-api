using Microsoft.EntityFrameworkCore;

namespace data_process_api.Models.Context {
    public class FuncionarioContext : DbContext {

        public FuncionarioContext(DbContextOptions<FuncionarioContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>().ToTable("Funcionarios");
            modelBuilder.Entity<Funcionario>().HasKey(p => p.Id);
        }

    }
}
