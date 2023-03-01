using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JardinesEdti2022.Web.Models.ViewModels.Carrito
{
    public class CarritoFinalizarCompra
    {
        public List<CarritoListVm> ItemsCarrito { get; set; }

        [Required(ErrorMessage = "El país es requerido")]
        [Display(Name = "País")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisEnvioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una ciudad")]
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "La ciudad es requerida")]
        public int CiudadEnvioId { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]

        public string DireccionEnvio { get; set; }
        [Display(Name = "Cod.Postal")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]

        public string CodigoPostalEnvio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]
        [Display(Name = "Contacto")]

        public string ContactoEnvio { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]
        public string TelefonoEnvio { get; set; }

        public List<SelectListItem> Paises { get; set; }
        public List<SelectListItem> Ciudades { get; set; }


    }
}