using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class Post:DbEntity
    {
        [Required,StringLength(2048)]
        public string Detail{get;set;}
        public DateTime Timestamp{get;set;}
        [Required]
        public int WebinarMeetingId{get;set;}
        [Required]
        public int ParticipantId{get;set;}
    }
}