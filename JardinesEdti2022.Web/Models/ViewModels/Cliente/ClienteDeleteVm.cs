using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JardinesEdti2022.Web.Models.ViewModels.Cliente
{
    public class ClienteDeleteVm
    {
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Cod. Postal")]
        public string CodigoPostal { get; set; }
        [Display(Name = "País")]
        public string Pais { get; set; }
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }
        public string Email { get; set; }

    }
}