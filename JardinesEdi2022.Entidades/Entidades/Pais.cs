using System.Collections.Generic;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Pais
    {
        public Pais()
        {
            Clientes = new HashSet<Cliente>();
            Proveedores = new HashSet<Proveedor>();
        }

        public int PaisId { get; set; }
        public string NombrePais { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Proveedor> Proveedores { get; set; }
    }
}
