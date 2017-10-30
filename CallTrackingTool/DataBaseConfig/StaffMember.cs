using System.Collections.Generic;

namespace CallTrackingTool

{
    public class StaffMember
    {
        // Primary Key
        public int Id { get; set; }

        // Navigation to calls
        public ICollection<Call> Calls { get; set; }

        //Navigation to Reports
        public ICollection<Reports> Reports { get; set; }

        public string StaffFirstName { get; set; }
        public string StaffLastName { get; set; }

    }
}
