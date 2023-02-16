using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Ciudad
{
    public class CiudadListVm
    {
        public int CiudadId { get; set; }

        [Display(Name = "Ciudad")]
        public string NombreCiudad { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }

    }
}