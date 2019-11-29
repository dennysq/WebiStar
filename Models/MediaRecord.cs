using System;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class MediaRecord : DbEntity
    {
        [Required, StringLength(64)]
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int WebinarMeetingId { get; set; }

        public WebinarMeeting WebinarMeeting { get; set; }

        public DateTime ExpireDate { get; set; }

    }
}