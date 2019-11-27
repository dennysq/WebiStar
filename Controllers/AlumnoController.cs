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
    public class AlumnoController : Controller
    {
        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }


        [Route("Alumno/Index")]
        [Route("Alumno/Index/{alumnoId}")]
        public IActionResult Index(string alumnoId)
        {
            if (!string.IsNullOrWhiteSpace(alumnoId))
            {
                var alumno = from alumn in _context.Alumnos
                                 where alumn.Id == alumnoId
                                 select alumn;
                return View(alumno.SingleOrDefault());
            }
            else
            {
                return View("MultiAlumno", _context.Alumnos);


            }



        }
        public IActionResult MultiAlumno()
        {



            return View(_context.Alumnos);


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
