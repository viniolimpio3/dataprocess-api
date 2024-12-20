﻿using Microsoft.EntityFrameworkCore;

namespace data_process_api.Models.Context {
    public class DatabaseContext : DbContext{
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abastecimento> Abastecimentos { get; set; }
        public DbSet<CarroFuncionario> CarroFuncionarios { get; set; }
        public DbSet<EmpresaCliente> EmpresasCliente { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }
        public DbSet<RepasseMotorista> RepassesMotorista { get; set; }
        public DbSet<Frete> Fretes { get; set; }
        public DbSet<Manutencao> Manutencoes { get; set; }
        public DbSet<PrecoPadrao> PrecosPadrao { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Saida> Saidas { get; set; }
        public DbSet<TipoEntrada> TiposEntrada { get; set; }
        public DbSet<TipoFuncionario> TiposFuncionario { get; set; }
        public DbSet<TipoSaida> TiposSaida { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>().ToTable("funcionarios");
            modelBuilder.Entity<Funcionario>().HasKey(p => p.Id);

            modelBuilder.Entity<Carro>().ToTable("carros");
            modelBuilder.Entity<Carro>().HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);

            modelBuilder.Entity<Abastecimento>().ToTable("abastecimentos");
            modelBuilder.Entity<Abastecimento>().HasKey(p => p.Id);

            modelBuilder.Entity<CarroFuncionario>().ToTable("carrosfuncioanarios");
            modelBuilder.Entity<CarroFuncionario>().HasKey(p => p.Id);

            modelBuilder.Entity<EmpresaCliente>().ToTable("empresascliente");
            modelBuilder.Entity<EmpresaCliente>().HasKey(p => p.Id);

            modelBuilder.Entity<Entrada>().ToTable("entradas");
            modelBuilder.Entity<Entrada>().HasKey(p => p.Id);

            modelBuilder.Entity<Fornecedor>().ToTable("fornecedores");
            modelBuilder.Entity<Fornecedor>().HasKey(p => p.Id);

            modelBuilder.Entity<Frete>().ToTable("fretes");
            modelBuilder.Entity<Frete>().HasKey(p => p.Id);

            modelBuilder.Entity<RepasseMotorista>().ToTable("repassemotorista");
            modelBuilder.Entity<RepasseMotorista>().HasKey(p => p.Id);

            modelBuilder.Entity<FormaPagamento>().ToTable("formaspagamento");
            modelBuilder.Entity<FormaPagamento>().HasKey(p => p.Id);

            modelBuilder.Entity<Manutencao>().ToTable("manutencoes");
            modelBuilder.Entity<Manutencao>().HasKey(p => p.Id);

            modelBuilder.Entity<PrecoPadrao>().ToTable("precospadrao");
            modelBuilder.Entity<PrecoPadrao>().HasKey(p => p.Id);

            modelBuilder.Entity<Regiao>().ToTable("regiao");
            modelBuilder.Entity<Regiao>().HasKey(p => p.Id);

            modelBuilder.Entity<Saida>().ToTable("saidas");
            modelBuilder.Entity<Saida>().HasKey(p => p.Id);

            modelBuilder.Entity<TipoEntrada>().ToTable("tiposentrada");
            modelBuilder.Entity<TipoEntrada>().HasKey(p => p.Id);

            modelBuilder.Entity<TipoFuncionario>().ToTable("tiposfuncionario");
            modelBuilder.Entity<TipoFuncionario>().HasKey(p => p.Id);

            modelBuilder.Entity<TipoSaida>().ToTable("tipossaida");
            modelBuilder.Entity<TipoSaida>().HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);


        }
    }
}
