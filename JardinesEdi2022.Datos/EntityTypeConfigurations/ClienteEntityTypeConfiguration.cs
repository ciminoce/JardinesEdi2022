using System.Data.Entity.ModelConfiguration;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfiguration:EntityTypeConfiguration<Cliente>
    {
        public ClienteEntityTypeConfiguration()
        {
            ToTable("Clientes");
        }
    }
}
