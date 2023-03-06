using System;
using System.Collections.Generic;
using System.Linq;
using JardinesEdi2022.Entidades.Enums;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Orden
    {
        public Orden()
        {
            DetalleOrdenes = new HashSet<DetalleOrden>();
        }
        public int OrdenId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaCompra { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }

        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string TransaccionId { get; set; }
        public decimal Total { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }

    }
}
