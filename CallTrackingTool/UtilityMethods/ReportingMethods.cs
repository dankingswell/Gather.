using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CallTrackingTool
{
    // Methods to generate reports
    public static partial class ReportingMethods
    {
        public static Tuple<string,string,List<Call>> AllCalls(CallLoggerDBContext access, StaffMember staff)
        {
            // Gathers report Name
            Console.WriteLine("Please enter a name for your report");
            var filename = Console.ReadLine().PrepareStringData();

            // Eager Loads all Calls in suitable format for  Calls Many to Many with Reports
            var Calls = access.Calls.Where(C => C.Id > 0).ToList();

            // Filters required fields from Calls and passes them into a string separated by ,'s
            var joinQuery = Calls.Select(c => string.Format("{0},{1},{2},{3},{4}", c.Id, c.CallInitiated, c.CallReason, c.CallNotes, c.StaffId));
            // joins all individual call strings with ---
            var Joined = string.Join(" --- ", joinQuery);

            return new Tuple<string, string, List<Call>>(Joined, filename, Calls);

        }

        public static Tuple<string,string> AllClients(CallLoggerDBContext access, StaffMember staff)
        {
            // Gathers report Name
            Console.WriteLine("Please enter a name for your report");
            var filename = Console.ReadLine().PrepareStringData();

            // Eager Loads all Clients 
            var Clients = access.Client.Where(C => C.Id > 0).ToList();

            // Filters required fields from Clients and passes them into a string separated by ,'s
            var joinQuery = Clients.Select(c => string.Format("{0},{1},{2},{3}", c.Id, c.FirstName, c.LastName, c.PostCode));
            // joins all individual call strings with ---
            var Joined = string.Join(" --- ", joinQuery);

            return new Tuple<string, string>( Joined, filename);
            
        }

        public static Tuple<string, string,List<Call>> AllCallsAllClients(CallLoggerDBContext access, StaffMember staff)
        {
            // Gathers report Name
            Console.WriteLine("Please enter a name for your report");
            var filename = Console.ReadLine().PrepareStringData();

            // eager Loads all Calls in suitable format for Calls Many to Many with Reports and includes client information to prevent N+1
            var Calls = access.Calls.Include(s => s.Client).Where(C => C.Id > 0).ToList();

            // Filters required fields from Calls and passes them into a string separated by ,'s
            var joinQuery = Calls.Select(c => string.Format("{0},{1},{2},{3},{4},{5}", c.Client.Id, c.Client.FirstName, c.Client.LastName, c.CallInitiated, c.CallReason, c.CallNotes));
            // joins all individual call strings with ---
            var Joined = string.Join(" --- ", joinQuery);

            return new Tuple<string, string, List<Call>>(Joined, filename, Calls);

        }


    }
}