using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Datos.Facades;
using JardinesEdi2022.Entidades.Entidades;

namespace JardinesEdi2022.Datos.Repositorios
{
    public class CarritosRepositorio : ICarritosRepositorio
    {
        private readonly ViveroSqlDbContext context;

        public CarritosRepositorio(ViveroSqlDbContext context)
        {
            this.context = context;
        }

        public void AgregarAlCarrito(int clienteId, int productoId)
        {
            try
            {
                var productoInCarrito =
                    context.Carritos
                        .SingleOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);
                if (productoInCarrito == null)
                {
                    Carrito carrito = new Carrito()
                    {
                        ProductoId = productoId,
                        ClienteId = clienteId,
                        Cantidad = 1
                    };
                    context.Carritos.Add(carrito);
                }
                else
                {
                    productoInCarrito.Cantidad += 1;
                    context.Entry(productoInCarrito).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int CantidadEnCarrito(int clienteId)
        {
            try
            {
                return context.Carritos.Count(c => c.ClienteId == clienteId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void QuitarDelCarrito(int clienteId, int productoId)
        {
            try
            {
                var carritoInDb =
                    context.Carritos.SingleOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);
                context.Entry(carritoInDb).State = EntityState.Deleted;



            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Carrito> GetItemsCarrito(int clienteId)
        {
            try
            {
                return context.Carritos
                    .Include(c => c.Producto)
                    .Where(c => c.ClienteId == clienteId).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Carrito GetItem(int clienteId, int productoId)
        {
            try
            {
                return context.Carritos
                    .SingleOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void VaciarCarrito(int clienteId)
        {
            try
            {
                var listaCarrito = GetItemsCarrito(clienteId);
                foreach (var carrito in listaCarrito)
                {
                    QuitarDelCarrito(clienteId, carrito.ProductoId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AgregarAlCarrito(int clienteId, int productoId, int cantidad)
        {
            try
            {
                var productoInCarrito =
                    context.Carritos
                        .SingleOrDefault(c => c.ClienteId == clienteId && c.ProductoId == productoId);
                if (productoInCarrito == null)
                {
                    Carrito carrito = new Carrito()
                    {
                        ProductoId = productoId,
                        ClienteId = clienteId,
                        Cantidad = cantidad
                    };
                    context.Carritos.Add(carrito);
                }
                else
                {
                    productoInCarrito.Cantidad += cantidad;
                    context.Entry(productoInCarrito).State = EntityState.Modified;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
