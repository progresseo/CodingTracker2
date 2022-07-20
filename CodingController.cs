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
                        // ActionController.CreateEntry(session);
                        CreateEntry();
                        break;
                    case "2":
                        ActionController.DisplayAll();
                        break;
                    case "3":
                        DeleteEntry();
                        break;
                    case "4":
                        UpdateEntry();
                        break;
                    default:
                        Console.WriteLine("Please ensure to enter a number between 1 to 4.");
                        break;
                }
            }
        }

        public static void CreateEntry()
        {
            CodingSession session = new CodingSession();
            session.Date = UserInput.GetDateInput();
            session.StartTime = UserInput.GetStartTime();
            session.EndTime = UserInput.GetEndTime();
            session.Duration = UserInput.DurationCalculator(session.StartTime, session.EndTime);
            ActionController.Add(session);
        }
        public static void DeleteEntry()
        {
            ActionController.DisplayAll();
            var recordId = UserInput.GetRecordId("Please enter the record Id you want to delete or enter 0 to return to main menu.");

            ActionController.Remove(recordId);
        }

        public static void UpdateEntry()
        {
            ActionController.DisplayAll();
            int recordID = UserInput.GetRecordId("Please enter the record Id you want to update or enter 0 to return to main menu.");
            ActionController.UpdateCheck(recordID);
            CodingSession session = new CodingSession();
            session.Id = recordID;
            session.Date = UserInput.GetDateInput();
            session.StartTime = UserInput.GetStartTime();
            session.EndTime = UserInput.GetEndTime();
            session.Duration = UserInput.DurationCalculator(session.StartTime, session.EndTime);
            ActionController.Update(session);
        }



    }
}
