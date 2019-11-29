using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class UserRoleMapping:DbEntity
    {
        [Required]
        public int RoleId{get;set;}
        [Required]
        public int UserId{get;set;}
    }
}