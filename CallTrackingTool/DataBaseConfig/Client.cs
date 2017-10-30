using System.Collections.Generic;

namespace CallTrackingTool
{
    public class Client
    {
        // Primary Key
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }


        // Navigation to Call
        public ICollection<Call> Calls { get; set; }


    }
}