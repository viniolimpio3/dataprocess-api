using Microsoft.EntityFrameworkCore;

namespace data_process_api.Models.Context {
    public class CarroFuncionarioContext : DbContext {

        public CarroFuncionarioContext(DbContextOptions<CarroFuncionarioContext> options) : base(options) { }

        public DbSet<CarroFuncionario> CarrosFuncionario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarroFuncionario>().ToTable("CarrosFuncionario");
            modelBuilder.Entity<CarroFuncionario>().HasKey(p => p.Id);
            
            modelBuilder.Entity<CarroFuncionario>()
            .HasMany(e => e.Funcionario)

            modelBuilder.Entity<Blog>()
        .HasMany(e => e.Posts)
        .WithOne(e => e.Blog)
        .HasForeignKey(e => e.BlogId)
        .HasPrincipalKey(e => e.Id);
        }

    }
}
