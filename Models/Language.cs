using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Language
    {
        [StringLength(32)]
        public string Name{get;set;}
[StringLength(10)]
        public string Id{get;set;}
    }
}