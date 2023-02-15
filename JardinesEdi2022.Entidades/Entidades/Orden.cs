using System;
using System.Collections.Generic;
using System.Linq;

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
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaEnvío { get; set; }

        public string DireccionEnvio { get; set; }
        public string CodigoPostalEnvio { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }

        public decimal TotalVenta => DetalleOrdenes.Sum(d => d.PrecioUnitario * (decimal)d.Cantidad);
    }
}
