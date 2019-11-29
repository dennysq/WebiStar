using System;

namespace asp_net_core.Models
{
    public class InteractionLog : DbEntity
    {
        public string Event { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public int WebinarMeetingId { get; set; }
        /*
          ,[INTERACTION_LOG_EVENT]
      ,[INTERACTION_LOG_TIMESTAMP]
      ,[INTERACTION_LOG_DESCRIPTION]
      ,[INTERACTION_LOG_MEETING_ID]
      */
    }
}