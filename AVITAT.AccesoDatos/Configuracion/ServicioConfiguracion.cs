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
    public class ServicioConfiguracion:IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreLocal).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.Precio).IsRequired().HasPrecision(18,2);
            builder.Property(x => x.HorarioAtencion).IsRequired();
        }
    }
}
