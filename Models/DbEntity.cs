using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace asp_net_core.Models
{
    public abstract class DbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Modified { get; set; }

    }
}