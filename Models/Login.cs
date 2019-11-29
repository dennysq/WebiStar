using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Login : DbEntity
    {
        [Required, StringLength(128)]
        public string PasswordHash { get; set; }
        [Required, StringLength(128)]
        public string Username { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }
    }
}