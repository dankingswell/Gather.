using System.Collections.Generic;
using System.Linq;
using System;

namespace CallTrackingTool
{
    public class Customer
    {
        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        private List<Client> ClientsAtPostCode;

        private CallLoggerDBContext access;
        private UI _ui;

        public IQueryable client;

       

        public Customer(string fName, string LName,string PostCode, UI ui)
        { // initialise UI and DB access Fields
            _ui = ui;
            access = _ui.access;
        
            // Runs Constructor as a method to allow recursion on invalid inputs
            EnterClientInfo(fName, LName, PostCode);
        }


        private void EnterClientInfo(string fName, string LName, string PostCode)
        {
            // eager loads for clients currently residing at the entered postcode
            ClientsAtPostCode = access.Client.Where(c => c.PostCode == PostCode).ToList();

            // Checks for matches from clients at residence
            var ExisitingResidentCheck = ClientsAtPostCode.Where(c => c.FirstName == fName && c.LastName == LName);
            if (ExisitingResidentCheck.Count() == 0)
            {
                // if no matches client is added directly to database. user is alerted when finished
                DatabaseAccessMethods.AddToClientDataBase(access, new Client { FirstName = fName, LastName = LName, PostCode = PostCode });
                Console.WriteLine($"Client {fName},{LastName} has been added to the Database at postcode {PostCode}");
            }
            else
            {
                // if client already exists options are given to client on how to proceed
                Console.WriteLine("Client already exists at this address");
                ReEnterInfo(fName,LastName,PostCode);
            };
        }

        private void ReEnterInfo(string FName,string LName, String Postcode)
        {
            // gives options for entering name again
            UtilityMethods.ReEnterNameOptions();

            //takes selection and passes through switch 
           int selection =  UtilityMethods.SelectionToNumber();
            switch (selection)
            {
                // returns user to homepage
                case 0:
                    Console.WriteLine("Returning to homepage");
                    break;
                // Gather new client info and passes for to creation method
                case 1:
                     var details = UtilityMethods.FirstNameLastNameAndOptionalPostCode(true);
                    EnterClientInfo(details.Item1, details.Item2, details.Item3);
                    break;
                // Defaults to asking for a valid selection
                default:
                    UtilityMethods.SelectionToNumber(true);
                    break;
            }
            
        }
        // Static Method to enable new clients that have been passed during log process to be added to the database without need to repeat client info
        public static void AddClientorFlagExisting(string fName, string LName, string PostCode, CallLoggerDBContext access)
        {
            // eager loads  any residence currently at postcode
            var ClientsAtPostCode = access.Client.Where(c => c.PostCode == PostCode).ToList();

            // checks for anymatches in current residence list
            var ExisitingResidentCheck = ClientsAtPostCode.Where(c => c.FirstName == fName && c.LastName == LName);

            if (ExisitingResidentCheck.Count() == 0)
            {
                // if client is new client is added to Database
                DatabaseAccessMethods.AddToClientDataBase(access, new Client { FirstName = fName, LastName = LName, PostCode = PostCode });
                // user is then informed
                Console.WriteLine($"Client {fName},{LName} has been added to the Database at postcode {PostCode}");
            }
        }


    }


}
