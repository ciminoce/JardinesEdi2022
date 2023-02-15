using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdti2022.Web.Models.ViewModels.Pais;

namespace JardinesEdti2022.Web.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        private readonly IPaisesServicios _paisesServicios;
        private readonly IMapper _mapper;

        public PaisController(IPaisesServicios paisesServicios)
        {
            _paisesServicios = paisesServicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index()
        {

            var listaPaisesVm = _mapper.Map<List<PaisListVm>>(_paisesServicios.GetLista());
            
            return View(listaPaisesVm);
        }
    }
}