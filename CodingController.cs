using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTracker2
{
    class CodingController
    {
        //Main Menu
        public static void MainMenu()
        {


            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {

                Console.WriteLine("Please select an option");
                Console.WriteLine("Enter 1 to create an entry.");
                Console.WriteLine("Enter 2 to display all entries.");
                Console.WriteLine("Enter 3 to delete an entry");
                Console.WriteLine("Enter 4 to update an entry");

                string optionSelected = Console.ReadLine();
                switch (optionSelected)
                {
                    case "0":
                        closeApp = true;
                        Environment.Exit(0);

                        break;
                    case "1":
                        //Console.WriteLine("testing");
                        //UserInput.GetStartTime();
                        //UserInput.DurationCalculator();
                       ActionController.CreateEntry();
                        break;
                    case "2":
                        ActionController.DisplayAll();
                        break;
                    case "3":
                       // Delete();
                        break;
                    case "4":
                       // Update();
                        break;
                    default:
                        Console.WriteLine("Please ensure to enter a number between 1 to 4.");
                        break;
                }
            }
        }
    }
}
