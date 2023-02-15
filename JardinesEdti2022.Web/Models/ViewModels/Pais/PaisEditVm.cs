using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Pais
{
    public class PaisEditVm
    {
        public int PaisId { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El nombre del país es requerido")]
        [StringLength(50,ErrorMessage = "El país debe tener entre {2} y {1} caracteres",MinimumLength = 3)]
        public string NombrePais { get; set; }


    }
}