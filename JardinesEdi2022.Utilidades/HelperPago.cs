using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Entidades.Enums;

namespace JardinesEdi2022.Utilidades
{
    public static class HelperPago
    {
        public static Tuple<string, EstadoOrden> ProcesarPago(decimal totalAPagar)
        {
            string transaccionId = Guid.NewGuid().ToString().Substring(0, 15);
            int estadoOrden = new Random().Next(1, 3);
            return new Tuple<string, EstadoOrden>(transaccionId, (EstadoOrden) estadoOrden);
        }
    }
}
