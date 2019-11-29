using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class MTimeZone:DbEntity
    {
        [Required,StringLength(64)]
        public string Name{get;set;}
        [Required]
        public string UtcOffset{get;set;}
        [Required]
        public bool Dst{get;set;}

    }
}