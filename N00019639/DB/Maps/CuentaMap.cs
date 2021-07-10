using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.DB.Maps
{
    public class CuentaMap : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuentas");
            builder.HasKey(o => o.Id);

            // builder.HasMany(o => o.Mov).WithOne(o => o.CuentaDestino).HasForeignKey(o => o.CuentaDestinoId);
        }
    }
}
