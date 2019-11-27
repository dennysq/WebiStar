using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {
        [Required (ErrorMessage="El atributo {0} es requerido")]
        [StringLength(5)]
        public override string Nombre{get;set;}
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }
        public string EscuelaId { get; set; }
        public Escuela Escuela { get; set; }
        [Required]
        [Display (Prompt="Dirección correspondencia", Name="Address")]
        [MinLength(10, ErrorMessage="La longitud mínima de la dirección es 10")]
        public string Dirección { get; set; }


        public void LimpiarLugar()
        {

            Console.WriteLine("Limpiando Establecimiento...");
            Console.WriteLine($"Curso {Nombre} Limpio");
        }
    }
}