namespace CallTrackingTool
{
    // Add to Database Methods
    public static partial class DatabaseAccessMethods
    {

        // Access' and adds Client data
        public static void AddToClientDataBase(CallLoggerDBContext access, Client client)
        {
            access.Client.Add(client);
            access.SaveChanges();
        }

        // Access' and adds StaffMember data
        public static void AddToStaffMemberDataBase(CallLoggerDBContext access, StaffMember staff)
        {
            access.StaffMember.Add(staff);
            access.SaveChanges();
        }

        // Access' and adds Call data
        public static void AddToCallDatabase(CallLoggerDBContext access, Call call)
        {
            access.Calls.Add(call);
            access.SaveChanges();
        }

        // Access' and adds Report data
        public static void AddToReportsDatabase(CallLoggerDBContext access, Reports report)
        {
            access.Reports.Add(report);
            access.SaveChanges();
        }
    }
}



