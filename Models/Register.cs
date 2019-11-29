using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Register:DbEntity
    {
        [Required]
        public DateTime Date{get;set;}
        [Required]
        public int ParticipantId{get;set;}
        [Required]
        public int WebinarMeetingId{get;set;}
        
    }
}