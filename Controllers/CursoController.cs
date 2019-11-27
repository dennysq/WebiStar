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
    public class CursoController : Controller
    {
        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
            _context = context;
        }


 [Route("Curso")]
        [Route("Curso/Index")]
        [Route("Curso/Index/{cursoId}")]
        public IActionResult Index(string cursoId)
        {
            if (!string.IsNullOrWhiteSpace(cursoId))
            {
                var curso = from c in _context.Cursos
                            where c.Id == cursoId
                            select c;
                return View(curso.SingleOrDefault());
            }
            else
            {
                return View("MultiCurso", _context.Cursos);


            }



        }
        public IActionResult Create()
        {



            return View();


        }
        public IActionResult Edit(string id)   
        {
            if(!string.IsNullOrWhiteSpace(id)){

                var curso = _context.Cursos.Find(id);

                return View(curso);
            }else{
                return View("MultiCurso", _context.Cursos);
            }
        }
        [HttpPost]
        public IActionResult Create(Curso curso)
        {

            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();
                curso.EscuelaId = escuela.Id;
                _context.Cursos.Add(curso);
                _context.SaveChanges();
                ViewBag.MensajeExtra="Curso Creado";
                return View("Index",curso);

            }else{
                return View(curso);
            }


            


        }
         [HttpPost]
        public IActionResult Edit(Curso curso)
        {

            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();
                curso.EscuelaId = escuela.Id;
                _context.Cursos.Update(curso);
                _context.SaveChanges();
                ViewBag.MensajeExtra="Curso Actualizado";
                return View("Index",curso);

            }else{
                return View(curso);
            }


            


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
