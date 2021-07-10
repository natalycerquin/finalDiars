using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.DB.Maps
{
    public class UsuarioAmigoMap : IEntityTypeConfiguration<UsuarioAmigo>
    {
        public void Configure(EntityTypeBuilder<UsuarioAmigo> builder)
        {
            builder.ToTable("UsuarioAmigos");
            builder.HasKey(o => new { o.UsuarioId, o.AmigoId});

            builder.HasOne(o => o.Usuario).WithMany(o => o.UsuarioAmigos).HasForeignKey(o => o.UsuarioId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
