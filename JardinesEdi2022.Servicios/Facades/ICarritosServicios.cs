using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Servicios.Facades
{
    public interface ICarritosServicios
    {
        //void AgregarAlCarrito(int clienteId, int productoId);
        int CantidadEnCarrito(int clienteId);
        void QuitarDelCarrito(int clienteId, int productoId);
        List<Carrito> GetItemsCarrito(int clienteId);
        void VaciarCarrito(int clienteId);
        Carrito GetItem(int clienteId, int productoId);
        void AgregarAlCarrito(int clienteId, int productoId, int cantidad);
    }
}
