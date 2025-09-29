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
    public class AveConfiguracion:IEntityTypeConfiguration<Ave>
    {
        public void Configure(EntityTypeBuilder<Ave> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreComun).IsRequired();
            builder.Property(x => x.NombreCientifico).IsRequired();
            builder.Property(x => x.Tipo).IsRequired();
            builder.Property(x => x.Descripcion).IsRequired();
            builder.Property(x => x.ImagenUrl).IsRequired(false);

        }
    }
}
