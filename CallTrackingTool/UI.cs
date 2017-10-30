using System;
using System.Collections.Generic;
using System.Linq;

namespace CallTrackingTool
{
    public class UI
    {



        public CallLoggerDBContext access = new CallLoggerDBContext();

        // gives option based access to void methods listed
        public Dictionary<int, Action<StaffMember,UI>> _StaffOptions = new Dictionary<int, Action<StaffMember,UI>>()
        {
            { 0, _end },
            { 1, _LogCall },
            { 2, _enterCallAgent },
            { 3, _EnterClient },
            { 4, _Reporting }
            
        };

        // requests login upon start of program, adds user to Db if new
        public void Login()
        {
          var user = UtilityMethods.FirstNameLastNameAndOptionalPostCode();
          var matches = access.StaffMember.Where(S => S.StaffFirstName == user.Item1 && S.StaffLastName == user.Item2).ToList();

          if(matches.Count() == 0)
            {
                Start(user,2);
            }
          else
            {
                //var StaffMember = new StaffMember { StaffFirstName = user.Item1, StaffLastName = user.Item2 };

                Start(matches.First());
            }

        }




        public void Start(StaffMember Staff)
        {
            // displays options for user

            Console.WriteLine("\n Please select one of the options below");
            Console.WriteLine("1. Log a Call \t 2. Add an Employee \t 3. Enter a Client");
            Console.WriteLine("4. Open Reporting Dashboard \t 0. Complete Session");

            // Takes user selection, verifys then sends to call relevant method
            int ConvertedSelection = UtilityMethods.SelectionToNumber();
            
            //if invalid key is selected reverts
            if (!_StaffOptions.ContainsKey(ConvertedSelection))
            {
                Console.WriteLine("Please select a valid option");
                Start(Staff);
            }
            else
            {
                var MethodToUse = _StaffOptions[ConvertedSelection];
                MethodToUse(Staff,this);

                if (ConvertedSelection != 0) { Start(Staff); }
                else { MethodToUse(Staff,this);
                    
                }

            }
        }

        public void Start(Tuple<string, string, string> User, int option = 0)
        {
            if (option == 2)
            {
                _enterCallAgent(User , this);
            }

            var Staff = access.StaffMember.Single(s => s.StaffLastName == User.Item2);
            Start(Staff);
            
        }






        //Dictionary Methods

        private static Action<StaffMember,UI> _end = (StaffMember s, UI ui) => Console.WriteLine("Completed Successfully");

        private static void _enterCallAgent(StaffMember Staff, UI ui)
        {
            Console.WriteLine("please enter an agents first and last name");
            var Names = UtilityMethods.FirstNameLastNameAndOptionalPostCode();
            new CallAgent(Names.Item1,Names.Item2,ui);
        }

        private static void _EnterClient(StaffMember Staff, UI ui)
        {
            Console.WriteLine("Please enter the clients first,last name and post code");
            var details = UtilityMethods.FirstNameLastNameAndOptionalPostCode(true);
            new Customer(details.Item1,details.Item2,details.Item3,ui);
        }

        private static void _LogCall(StaffMember Staff,UI ui)
        { 
            new InboundCall(Staff,ui);
        }

        private static void _Reporting(StaffMember staff, UI ui)
        {
            new ReportingDashboard(staff ,ui);
        }


        // OverLoaded Methods

        // method allows new users to automatically login
        private static void _enterCallAgent(Tuple<string, string, string> User,UI ui)
        {
            new CallAgent(User.Item1, User.Item2,ui);
        }

        // Method allows new clients to be added during log process
        public void _EnterClient(Tuple<string, string, string> User, UI ui)
        {
           Customer.AddClientorFlagExisting(User.Item1, User.Item2, User.Item3,ui.access);
          
        }
        
    }
}
