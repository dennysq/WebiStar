using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Invitation:DbEntity
    {
        public string Name{get;set;}
        [Required]
        public string EmailAddress{get;set;}
        public string Subject{get;set;}
        public string Message{get;set;}
        public bool Accepted{get;set;}
        public int WebinarMeetingId{get;set;}


    }
}