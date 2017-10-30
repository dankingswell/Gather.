using System;
using System.Linq;
using System.Collections.Generic;

namespace CallTrackingTool
{
    public class InboundCall
    {
        private UI _ui;
        private CallLoggerDBContext access;
        private string _callReason { get; set; }
        private string _callNotes { get; set; }
        private DateTime _callInitiated = DateTime.Now;
        private Client _customer;
        private List<Client> ClientsAtPostCode;


        public InboundCall(StaffMember Employee,UI ui)
        {
            // initialises UI and DbAccess
            _ui = ui;
            access = _ui.access;

            // Gathers client info
            Console.WriteLine("Enter customer details:");
            var client = UtilityMethods.FirstNameLastNameAndOptionalPostCode(true);
            
            // Checks  if clients is new or existing. creates cliebt if new 
            CurrentOrNewClient(client, ui);

            // Gathers clients call information
            _callReason = UtilityMethods.LargeTextInput("Please enter reason for clients call");
            _callNotes = UtilityMethods.LargeTextInput("Please enter any notes regarding the call you would like to be stored.");

            // Saves call object containing gathered client information to data base
            DatabaseAccessMethods.AddToCallDatabase(access ,new Call() { StaffId = Employee.Id, CallInitiated = _callInitiated, ClientId = _customer.Id, CallReason = _callReason, CallNotes = _callNotes });
            Console.WriteLine("Call stored to database");

        }

        private void CurrentOrNewClient(Tuple<string,string,string> client, UI ui)
        {
            //Querys Eager loaded to find clients at postcode
            ClientsAtPostCode = ui.access.Client.Where(c => c.PostCode == client.Item3).ToList();

            // Checks to see if any of the residents at the postcode match the current client
            var ExisitingResidentCheck = ClientsAtPostCode.Where(c => c.FirstName == client.Item1 && c.LastName == client.Item2).ToList() ;

            if (ExisitingResidentCheck.Count() == 0)
            {
                // Alerts user if client is new, then passes client for creation

                Console.WriteLine("New customer");
                    ui._EnterClient(client,ui);
                

                // Ensures client is in Database
                _customer = ui.access.Client.Single(c => c.FirstName == client.Item1 && c.LastName == client.Item2 && c.PostCode == client.Item3);
            }
            else
            {
                // alerts user the client is exisiting and assigns customer field
                Console.WriteLine("Existing customer");
                _customer = ExisitingResidentCheck.First();
            }

        }

        


        

    }
}
