using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N00019639.DB.Maps;
using N00019639.Models;

namespace N00019639.DB
{
    public class AppYapeContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<UsuarioAmigo> UsuarioAmigos { get; set; }

        public AppYapeContext(DbContextOptions<AppYapeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new MovimientoMap());
            modelBuilder.ApplyConfiguration(new UsuarioAmigoMap());
            
        }
    }
}
