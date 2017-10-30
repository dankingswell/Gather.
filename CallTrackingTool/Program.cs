using System;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CallTrackingTool
{
    class Program
    {

        static void Main(string[] args)
        {
            // Opens Inital Ui
            var ui = new UI();
            // Starts login process
            ui.Login();
            Console.WriteLine("Closed Successfully");

        }
    }
}
