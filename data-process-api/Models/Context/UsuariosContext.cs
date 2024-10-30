using Microsoft.EntityFrameworkCore;

namespace data_process_api.Models.Context {
    public class UsuariosContext : DbContext {

        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);
        }
    }
}
