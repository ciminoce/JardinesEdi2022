using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Facades
{
    public interface ICarritosRepositorio
    {
        //void AgregarAlCarrito(int clienteId, int productoId);
        void AgregarAlCarrito(int clienteId, int productoId, int cantidad);
        int CantidadEnCarrito(int clienteId);
        void QuitarDelCarrito(int clienteId, int productoId);
        List<Carrito> GetItemsCarrito(int clienteId);
        Carrito GetItem(int clienteId, int productoId);
        void VaciarCarrito(int clienteId);
    }
}
