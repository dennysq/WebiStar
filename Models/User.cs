using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace asp_net_core.Models
{
    public class User : DbEntity
    {
        [Required, StringLength(128)]
        public string Username { get; set; }
        [Required, StringLength(128)]
        public string FirstName { get; set; }
        [StringLength(128)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(64)]
        public string CompanyName { get; set; }
        [StringLength(16)]
        public string PhoneNumber { get; set; }
        public Language Language { get; set; }
        public String LanguageId { get; set; }
public string PhotoUrl {get;set;}
public List<WebinarMeeting> webinarMeetings{get;set;}
    }
}