using System;
using System.Collections.Generic;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Ciudad
    {
        public Ciudad()
        {
            Clientes = new HashSet<Cliente>();
            Proveedores = new HashSet<Proveedor>();
        }

        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public int PaisId { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Proveedor> Proveedores { get; set; }
    }
}
