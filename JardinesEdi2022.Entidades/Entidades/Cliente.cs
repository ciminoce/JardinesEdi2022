using System;
using System.Collections.Generic;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Cliente
    {
        public Cliente()
        {
            Ordenes = new HashSet<Orden>();
        }

        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public string Email { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual ICollection<Orden> Ordenes { get; set; }
        public string ApeNombre => $"{Apellido}, {Nombres}";
    }
}
