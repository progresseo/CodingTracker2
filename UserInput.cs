using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodingTracker2
{
    class UserInput
    {
        public static int GetRecordId(string message)
        {
            Console.WriteLine(message);
            string idInput = Console.ReadLine();
            if (idInput == "0") CodingController.MainMenu();
            while (!Int32.TryParse(idInput, out _) || Convert.ToInt32(idInput) < 0)
            {
                Console.WriteLine("Please enter the redocd ID. Enter an integer e.g 4");
                idInput = Console.ReadLine();

            }
            int convertedIdInput = Convert.ToInt32(idInput);
            return convertedIdInput;
        }
        public static string GetDateInput()
        {
            Console.WriteLine("Please enter the date in format DD MMMM YYYY. Enter 0 for main menu");
            string dateInput = Console.ReadLine();
            if (dateInput == "0") CodingController.MainMenu();
            while (!DateTime.TryParseExact(dateInput, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Please enter the date in format DD MMMM YYYY e.g 05 May 2006");
                dateInput = Console.ReadLine();

            }

            return dateInput;
        }
        public static string GetStartTime()
        {
            Console.WriteLine("Enter Start time in format HH:mm e.g 01:20 Enter 0 for main menu");
            string startTimeInput = Console.ReadLine();
            if (startTimeInput == "0") CodingController.MainMenu();
            while (!DateTime.TryParseExact(startTimeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Enter Start Time in format HH:mm e.g 02:30 which is 2 hours and 30 minutes");
                startTimeInput = Console.ReadLine();

            }
            //Console.WriteLine(startTimeInput);
            return startTimeInput;
            
        }
        public static string GetEndTime()
        {
            Console.WriteLine("Enter End Time in format HH:mm e.g 01:20 Enter 0 for main menu");
            string endTimeInput = Console.ReadLine();
            if (endTimeInput == "0") CodingController.MainMenu();
            while (!DateTime.TryParseExact(endTimeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Enter End Time in format HH:mm e.g 02:30 which is 2 hours and 30 minutes");
                endTimeInput = Console.ReadLine();

            }
            //Console.WriteLine(endTimeInput);
            return endTimeInput;

        }

        public static string DurationCalculator(string startTime, string endTime)
        {
            var convertedStartTime = DateTime.Parse(startTime);
            var convertedEndTime = DateTime.Parse(endTime);
            TimeSpan difference = convertedEndTime - convertedStartTime;
            string duration = string.Format("{0} hours, {1} minues",  difference.Hours, difference.Minutes);
            Console.WriteLine(duration);
            return duration;
        }
    }
}
