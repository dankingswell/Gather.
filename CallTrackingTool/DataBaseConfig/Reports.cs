using System;
using System.Collections.Generic;

namespace CallTrackingTool
{
    public class Reports
    {
        public int Id { get; set; }

        // One to many relationship with Staff
        public StaffMember Staff { get; set; }

        // many To many Relationship with Calls
        public ICollection<Call> Calls { get; set; }

        public int StaffId { get; set; }

        public string Data { get; set; }
        public string FileName { get; set; }
        public DateTime RequestedTime { get; set; }





    }
}