using System;
using System.Collections.Generic;
using System.Linq;

namespace CallTrackingTool
{
    public class ReportingDashboard
    {
        
        private StaffMember staff;
        private UI ui;
        private CallLoggerDBContext access;

        public ReportingDashboard( StaffMember staff, UI ui)
        {
            this.staff = staff;
            this.ui = ui;
            access = ui.access;
            ReportingOptions(staff, ui);
        }

        private void ReportingOptions(StaffMember staff, UI ui)
        {
            // Displays Options for client on console
            Console.WriteLine("Which report would you like to run?");
            Console.WriteLine("1.All Calls \t 2.All Clients \t 3.All Clients All Calls");
            Console.WriteLine("0. Exit and return to previous menu");
            var choice = UtilityMethods.SelectionToNumber();


            if (choice == 1)
            {
                // Gathers All Calls Data
                var ReportData = ReportingMethods.AllCalls(access, staff);

                // Creates Report object with returned data
                var report = new Reports { StaffId = staff.Id, Data = ReportData.Item1, FileName = ReportData.Item2, RequestedTime = DateTime.Now, Calls = ReportData.Item3 };

                //Adds Report to DB and Alerts user when completed
                DatabaseAccessMethods.AddToReportsDatabase(access, report);
                Console.WriteLine($"Report {ReportData.Item2} saved in reports Database");
            }
            else if (choice == 2)
            {
                // Gathers All Clients Data
                var ReportData = ReportingMethods.AllClients(access, staff);

                // Creates Report object with returned data
                var report = new Reports { StaffId = staff.Id, Data = ReportData.Item1, FileName = ReportData.Item2, RequestedTime = DateTime.Now };

                //Adds Report to DB and Alerts user when completed
                DatabaseAccessMethods.AddToReportsDatabase(access, report);
                Console.WriteLine($"Report {ReportData.Item2} saved in reports Database");
            }

            else if (choice == 3)
            {
                // Gathers All Calls and All Clients Data
                var ReportData = ReportingMethods.AllCallsAllClients(access, staff);

                // Creates Report object with returned data
                var report = new Reports { StaffId = staff.Id, Data = ReportData.Item1, FileName = ReportData.Item2, RequestedTime = DateTime.Now, Calls = ReportData.Item3 };

                //Adds Report to DB and Alerts user when completed
                DatabaseAccessMethods.AddToReportsDatabase(access, report);
                Console.WriteLine($"Report {ReportData.Item2} saved in reports Database");
            }
            else
            {
                // invalid options are passed back through two selection
                Console.WriteLine("Please select a valid option");
                ReportingOptions(staff, ui);
            }
        }
    }
}