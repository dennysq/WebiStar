using System;
using System.Collections.Generic;

namespace asp_net_core.Models
{
    public class Asignatura:ObjetoEscuelaBase
    {
        public string CursoId{get;set;}
        public Curso Curso{get;set;}
 public List<Evaluación> Evaluaciones { get; set; } = new List<Evaluación>();

    }
}