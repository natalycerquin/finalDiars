using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.DB.Maps
{
    public class MovimientoMap : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimientos");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.CuentaDestino).WithMany(o => o.MovimientosSalida).HasForeignKey(o => o.CuentaDestinoId);
            builder.HasOne(o => o.CuentaOrigen).WithMany(o => o.MovimientosIngreso).HasForeignKey(o => o.CuentaOrigenId);
        }
    }
}
