using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool makeTest = true;
            while (makeTest)
            {
                try
                {
                    MakeTest();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("There were errors: " + ex.Message);

                }
                Console.WriteLine("Press 1 to make another test or press enter to exit.");
                makeTest = (Console.ReadLine() == "1" ? true : false);

            }
            
        }

        private static void MakeTest()
        {
            Console.WriteLine("Message to show: ");
            string Message = Console.ReadLine();

            Console.WriteLine("Message type[1-Message/2-Error/3-Warning]: ");
            string typeMessage = Console.ReadLine();

            Console.WriteLine("Log to Database [y/n] ");
            string _logToDatabase = Console.ReadLine();

            Console.WriteLine("Log to File [y/n] ");
            string _logToFile = Console.ReadLine();

            Console.WriteLine("Log to Console [y/n] ");
            string _logToConsole = Console.ReadLine();

            Console.WriteLine("Press enter to proced. ");
            Console.ReadLine();

            bool logToDatabase = (_logToDatabase.ToLower() == "y" ? true : false);
            bool logToFile = (_logToFile.ToLower() == "y" ? true : false);
            bool logToConsole = (_logToConsole.ToLower() == "y" ? true : false);
            bool isMessage = (typeMessage == "1" ? true : false);
            bool isError = (typeMessage == "2" ? true : false);
            bool isWarning = (typeMessage == "3" ? true : false);

            new JobLogger(logToFile, logToConsole, logToDatabase);
            JobLogger.LogMessage(Message, isMessage, isWarning, isError);

        }
    }
}
