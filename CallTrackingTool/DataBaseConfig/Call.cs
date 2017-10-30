    using System;
using System.Collections.Generic;

namespace CallTrackingTool
{
    public class Call
    {
        // PK
        public int Id { get; set; }
        // Call Time
        public DateTime CallInitiated { get; set; }

        // One To Many Relationship between Staff Members and Calls
        public StaffMember Staff { get; set; }

        // One To Many Relationship between Clients and Calls
        public Client Client { get; set; }

        // Many to Many Reporting Relationship
        public ICollection<Reports> Reports { get; set; }

        public string CallReason { get; set; }
        public string CallNotes { get; set; }
        


        // FK to Clients
        public int ClientId { get; set; }

        //FK to StaffMembers
        public int StaffId { get; set; }
    }
}