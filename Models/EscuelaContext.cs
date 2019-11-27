using System;
using System.Linq;
using System.Collections.Generic;
using asp_net_core.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_core
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreación = 1961;
            escuela.Nombre = "Platzi School";
            escuela.Id = Guid.NewGuid().ToString();
            escuela.TipoEscuela = TiposEscuela.Secundaria;
            escuela.Ciudad = "Riobamba";
            escuela.Pais = "Ecuador";
            escuela.Dirección = "Av Lizarzaburu";
            //cargar cursos
            var cursos = CargarCursos(escuela);
            //por cada curso cargar asignaturas
            var asignaturas = CargarAsignaturas(cursos);
            //por cada curso cargar alumnos
            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            
        }
        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            Random random = new Random();
            var listaCompleta = new List<Alumno>();
            foreach (var curso in cursos)
            {
                var limite = random.Next(5, 20);
                var tmpList = GenerarAlumnosAlAzar(curso, limite);
                //curso.Alumnos = tmpList;
                listaCompleta.AddRange(tmpList);
            }
            return listaCompleta;

        }
        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                var tempList = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas",
                            CursoId=curso.Id,
                            Id=Guid.NewGuid().ToString()} ,
                            new Asignatura{Nombre="Educación Física",CursoId=curso.Id, Id=Guid.NewGuid().ToString()},
                            new Asignatura{Nombre="Castellano", CursoId=curso.Id,Id=Guid.NewGuid().ToString()},
                            new Asignatura{Nombre="Ciencias Naturales", CursoId=curso.Id,Id=Guid.NewGuid().ToString()},
                            new Asignatura{Nombre="Programacion", CursoId=curso.Id,Id=Guid.NewGuid().ToString()}
                            };
               
                listaCompleta.AddRange(tempList);
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            var escCursos = new List<Curso>{
new Curso(){
    Id=Guid.NewGuid().ToString(),
    EscuelaId=escuela.Id,
    Nombre="101",
    Dirección="Av Siempre Viva",
    Jornada=TiposJornada.Mañana
},
new Curso(){
    Id=Guid.NewGuid().ToString(),
    EscuelaId=escuela.Id,
    Nombre="201",Dirección="Av Siempre Viva",
    Jornada=TiposJornada.Mañana
},new Curso(){
    Id=Guid.NewGuid().ToString(),
    EscuelaId=escuela.Id,
    Nombre="301",Dirección="Av Siempre Viva",
    Jornada=TiposJornada.Tarde
},new Curso(){
    Id=Guid.NewGuid().ToString(),
    EscuelaId=escuela.Id,
    Nombre="401",Dirección="Av Siempre Viva",
    Jornada=TiposJornada.Tarde
},new Curso(){
    Id=Guid.NewGuid().ToString(),
    EscuelaId=escuela.Id,
    Nombre="501",Dirección="Av Siempre Viva",
    Jornada=TiposJornada.Noche
}
};
            return escCursos;
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int limite)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   Nombre = $"{n1} {n2} {a1}",
                                   CursoId = curso.Id,
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(limite).ToList();
        }
    }

}