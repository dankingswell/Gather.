using System;
using System.Text.RegularExpressions;

namespace CallTrackingTool
{
    public static partial class UtilityMethods
    {


        // Takes the selection provided by Readline checks that the selection is a number and returns it.
        public static int SelectionToNumber(bool Repeatselection = false)
        {
            if (Repeatselection)
            {
                // if method is repeated false bool prompts valid selection text || if method is passed with false for validation text
                Console.WriteLine("Please make a valid selection");
            }
            // gathers selection
            string selection = Console.ReadLine();

            // checks that selection is a number a out's the conversion if so
            var check = Int32.TryParse(selection, out int ConvertedSelection);
            // if conversion from string is successful returns converted number else re passes to method with validation text request
            return check ? ConvertedSelection : SelectionToNumber(true);
        }

        // records large amount of text without additional checks
        public static string LargeTextInput(string VariableText = "")
        {
            // allows custom message to alert user of info required
            if (VariableText != "") { Console.WriteLine(VariableText); }
            // gathers text
            string Reason = Console.ReadLine().ToString();
            return Reason;
        }

        // Loop to check that user has confirmed information in valid format. caters to Yes or No by selecting 1st character.
        public static string CheckLoop(string answer)
        {
            // Prepares string and places into correct format, takes firstcharacter and converts for validation
            answer = answer.PrepareStringData()[0].ToString();

            if (answer != "Y" && answer != "N")
            {
                // incorrect answers are re-passed to method with validation message
                Console.WriteLine("Please respond with either the Letter Y or N to Indicate if the following information is correct");
                CheckLoop(Console.ReadLine());
            }
            return answer;
        }

        // Returns both names with an optional parameter of PostCode
        public static Tuple<string, string, string> FirstNameLastNameAndOptionalPostCode(bool postcode = false, string firstname = "", string lastname = "")
        {
            // initialised postcode, return values that do not use postcode will have an empty string.
            string Postcode = "";

            // if names are pre passed to method skips re entry else asks user for name
            if (firstname == "" && lastname == "")
            {
                Console.WriteLine("First Name:");
                firstname = Console.ReadLine().PrepareStringData();
                Console.WriteLine("Last Name:");
                lastname = Console.ReadLine().PrepareStringData();
            }

            // if postcode has been indicated as requested user is prompted for entry
            if (postcode)
            {
                Console.WriteLine("Postcode:");
                Postcode = Console.ReadLine().PrepareStringData();
            }

            // variable message based on boolean postcode value passed to method
            string message = postcode ? $"First Name: {firstname} Last Name: {lastname} Postcode: {Postcode} is that correct?  Y/N" : $"First Name: {firstname} Last Name: {lastname} is that correct?  Y/N";
            Console.WriteLine(message);
            // takes answer if entry is correct
            string answer = Console.ReadLine();

            //Ensures response if valid
            string Verifiedanswer = CheckLoop(answer);

            // passes user back through if N is selected else returns new Tuple object with values
            return Verifiedanswer == "N" && postcode ? FirstNameLastNameAndOptionalPostCode(true) :
                Verifiedanswer == "N" && !postcode ? FirstNameLastNameAndOptionalPostCode() :
                new Tuple<string, string, string>(firstname, lastname, Postcode);
        }

        // allows for re entry of name fields. also allows for multiple variable options
        public static void ReEnterNameOptions(params string[] variableText)
        {
            // standard name option
            Console.WriteLine("Would you like to \n1: enter another name.");
            // iterates over params if valid and displays options
            foreach (var item in variableText)
            {
                Console.WriteLine($"{item}");
            }
            // standard exit key
            Console.WriteLine("0: Exit.");
        }

        // takes name option. checks digits at the end and increments by one
        public static string IncrementLargestNumber(string Largestname)
        {
            // pattern to collect digits
            Regex Pattern = new Regex(@"\d+");

            // uses pattern to check for numbers
            var number = Pattern.Match(Largestname);

            // creates bool on attempt to parse the number, passes out value and increments by 1
            var ParseAttempt = Int32.TryParse(number.Value, out int value);
            value++;

            
            if (ParseAttempt)
            {
                // if parse was successful takes the length of the number and  - it from length the original string length.
                // then creates substring with just string values
                var NameWithoutNumber = Largestname.Substring(0, Largestname.Length - number.Value.ToString().Length);

                // appends new incremented number
                return NameWithoutNumber + value;

            }
            else
            {
                // if no number exists adds 1
                return Largestname + "1";
            }
        }
    }
}



