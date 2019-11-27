using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asp_net_core.Models;

namespace asp_net_core.Controllers
{
    public class AsignaturaController : Controller
    {
        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")]
        public IActionResult Index(string asignaturaId)
        {
            if (!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                 where asig.Id == asignaturaId
                                 select asig;
                return View(asignatura.SingleOrDefault());
            }
            else
            {
                return View("MultiAsignatura", _context.Asignaturas);


            }



        }


        public IActionResult MultiAsignatura()
        {



            return View("MultiAsignatura", _context.Asignaturas);


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
