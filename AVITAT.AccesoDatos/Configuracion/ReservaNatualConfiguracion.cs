using AVITAT.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVITAT.AccesoDatos.Configuracion
{
    public class ReservaNatualConfiguracion:IEntityTypeConfiguration<ReservaNatural>
    {
        public void Configure(EntityTypeBuilder<ReservaNatural> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Ubicacion).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
            builder.Property(x => x.Acceso).IsRequired();
        }
    }
}
