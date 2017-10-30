using System;
using System.Linq;
using System.Collections.Generic;

namespace CallTrackingTool
{
    public class CallAgent
    {
        private UI _ui;
        private CallLoggerDBContext access;
        private List<StaffMember> PreExistingCheck;
        private string FirstName;
        private string LargestName;


        public CallAgent(string FName, string LName, UI ui)
        {
            //Initialises ui and access fields
            _ui = ui;
            access = _ui.access;
            FirstName = FName;
            // calls constructor as method to allow for recursion for invalid inputs
            EnterInfo(FName, LName);
        }

        private void EnterInfo(string FName, string LName)
        {
            // Check for existing employee records with same last name, saves locally for re-use to reduce querys to DB
            PreExistingCheck = access.StaffMember.Where(s => s.StaffLastName.Contains(LName)).ToList();
            var ExactMatchCheck = PreExistingCheck.Where(c => c.StaffLastName == LName).Count();

            // If Agent is new adds user to database
            if (ExactMatchCheck == 0)
            {
                DatabaseAccessMethods.AddToStaffMemberDataBase(access, new StaffMember { StaffLastName = LName, StaffFirstName = FName });
                // alerts user when finished
                Console.WriteLine($"Staff member saved to database as {FName},{LName}.");
            }
            else
            {
                // re-requests info/navigation options
                ReEnterRequest(FName,LName);
            }



        }

        // Method to prompt user to re enter information and provides guidance on how to proceed
        private void ReEnterRequest(string FName, string LName)
        {
            // uses pre exisiting query logic and orders clients to get highest number recorded next to clients name
            var record = PreExistingCheck.OrderBy(c => c.StaffLastName);
            // takes last record e.g largest
            LargestName = record.Last().StaffLastName;

            // informs user of pre existing record and largest username
            Console.WriteLine($"The user you have entered already exists please enter the name again with either a number appended to the last name,the current highest user stored is {LargestName} \n");
            // gives client selection options with variable text + username information
            UtilityMethods.ReEnterNameOptions("2: automatically add 1 to the current highest username " + LargestName);

            // checks selection
            var Answer = UtilityMethods.SelectionToNumber();
            SelectionValidationLoop(Answer);
            

        }

        // selection loop to ensure valid option is selected
        private  void SelectionValidationLoop(int Answer)
        {
            if (Answer == 1)
            {
                // gives client re enter information
                var Names = UtilityMethods.FirstNameLastNameAndOptionalPostCode();
                EnterInfo(Names.Item1, Names.Item2);
            }
            else if (Answer == 2)
            {
                // gives client option to increment largest username by 1 to create valid username
                var Lname = UtilityMethods.IncrementLargestNumber(LargestName);
                EnterInfo(FirstName, Lname);

            }
            else if (Answer == 0)
            {
                // returns client to dashboard
                Console.WriteLine("Returning to previous menu");
            }
            else
            {
                // invalid selections are promted to be re-input
                UtilityMethods.ReEnterNameOptions("2: automatically add 1 to the current highest username " + LargestName);
                var selection = UtilityMethods.SelectionToNumber();
                SelectionValidationLoop(selection);

            }
        }

    }
}
