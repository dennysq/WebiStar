using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class WebinarMeeting : DbEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public MTimeZone MeetingTimeZone { get; set; }
        [StringLength(8)]
        public string Password { get; set; }
        public bool HostVideoEnabled { get; set; }

        public bool ParticipantVideoEnabled { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [Required, StringLength(128)]
        public string Description { get; set; }
        [Required, StringLength(128)]
        public string Name { get; set; }
        [StringLength(256)] public string BannerUrl { get; set; }
        public int UserId { get; set; }
        public decimal Price{get;set;} 
        public User User { get; set; }
    }
}