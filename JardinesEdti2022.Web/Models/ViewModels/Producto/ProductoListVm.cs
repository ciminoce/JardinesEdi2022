using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Producto
{
    public class ProductoListVm
    {
        public int ProductoId { get; set; }
        [Display(Name = "Producto")]
        public string NombreProducto { get; set; }
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }
        [Display(Name = "Precio")]

        public decimal PrecioUnitario { get; set; }
        [Display(Name = "Stock")]
        public int UnidadesEnStock { get; set; }
        public bool Suspendido { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }

    }
}