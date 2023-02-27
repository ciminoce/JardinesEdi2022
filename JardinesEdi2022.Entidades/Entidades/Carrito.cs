using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JardinesEdi2022.Entidades.Entidades
{
    public class Carrito
    {
        public int CarritoId { get; set; }
        public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public Cliente Cliente { get; set; }
        public Producto Producto { get; set; }

    }
}
