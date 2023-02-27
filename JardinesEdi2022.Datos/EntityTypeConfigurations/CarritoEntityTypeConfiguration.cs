using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.EntityTypeConfigurations
{
    public class CarritoEntityTypeConfiguration:EntityTypeConfiguration<Carrito>
    {
        public CarritoEntityTypeConfiguration()
        {
            ToTable("Carritos");
        }
    }
}
