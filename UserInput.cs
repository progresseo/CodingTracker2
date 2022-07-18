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
            return Validation.ValidateRecordId(idInput);
        }
        public static string GetDateInput()
        {
            Console.WriteLine("Please enter the date in format DD MMMM YYYY. Enter 0 for main menu");
            string dateInput = Console.ReadLine();
            if (dateInput == "0") CodingController.MainMenu();
            return Validation.ValidateDate(dateInput);
        }
        public static string GetStartTime()
        {
            Console.WriteLine("Enter Start time in format HH:mm e.g 01:20 Enter 0 for main menu");
            string startTimeInput = Console.ReadLine();
            if (startTimeInput == "0") CodingController.MainMenu();
            return Validation.ValidateStartTime(startTimeInput);
            
        }
        public static string GetEndTime()
        {
            Console.WriteLine("Enter End Time in format HH:mm e.g 01:20 Enter 0 for main menu");
            string endTimeInput = Console.ReadLine();
            if (endTimeInput == "0") CodingController.MainMenu();
            return Validation.ValidateEndTime(endTimeInput);

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
