using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JardinesEdti2022.Web.Models.ViewModels.Producto
{
    public class ProductoEditVm
    {
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50,ErrorMessage = "Debe contener entre {2} y {1} caracteres",MinimumLength = 3)]
        [Display(Name = "Producto")]
        public string NombreProducto { get; set; }

        [StringLength(50, ErrorMessage = "Debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre Latín")]
        public string NombreLatin { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido")]
        [Display(Name = "Proveedor")]
        [Range(1, int.MaxValue,ErrorMessage = "Debe seleccionar un proveedor")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Display(Name = "Precio Unit.")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser positivo")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Display(Name = "Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser positivo")]

        public int UnidadesEnStock { get; set; }

        [Required(ErrorMessage = "El nivel de reposición es requerido")]
        [Display(Name = "Nivel de Reposición")]
        [Range(1, int.MaxValue, ErrorMessage = "El nivel debe ser positivo")]
        public int NivelDeReposicion { get; set; }
        public bool Suspendido { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagenFile { get; set; }
        public List<SelectListItem> Categorias { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
    }
}