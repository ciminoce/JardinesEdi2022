using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JardinesEdti2022.Web.Models.ViewModels.Ciudad
{
    public class CiudadEditVm
    {
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El nombre de la ciudad es requerido")]
        [StringLength(50,ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres",MinimumLength = 3)]
        [Display(Name = "Ciudad")]
        public string NombreCiudad { get; set; }

        [Required(ErrorMessage = "El país es requerido")]
        [Display(Name = "País")]
        [Range(1, int.MaxValue,ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }

        public List<SelectListItem> Paises { get; set; }

    }
}