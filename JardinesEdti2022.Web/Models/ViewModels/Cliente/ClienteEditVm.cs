using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JardinesEdti2022.Web.Models.ViewModels.Cliente
{
    public class ClienteEditVm
    {
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]

        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]

        public string Apellido { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]
        public string Direccion { get; set; }

        [Display(Name = "Cod.Postal")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe estar comprendido entre {1} y {2} caracteres", MinimumLength = 3)]

        public string CodigoPostal { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        [Display(Name = "País")]
        [Required(ErrorMessage = "El país es requerido")]

        public int PaisId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una ciudad")]
        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "La ciudad es requerida")]

        public int CiudadId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El Mail es requerido")]
        [MaxLength(200, ErrorMessage = "El campo no puede contener más de 200 caracteres")]
        public string Email { get; set; }
        public List<SelectListItem> Ciudades { get; set; }
        public List<SelectListItem> Paises { get; set; }

   
    }
}