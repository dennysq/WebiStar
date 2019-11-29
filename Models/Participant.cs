using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Participant:DbEntity
    {
        [Required,StringLength(128)]
        public string Name{get;set;}
        [StringLength(64)]
        public string Origin{get;set;}
        [Required]
        public DateTime JoinDate{get;set;}
    }
}