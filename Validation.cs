using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodingTracker2
{
    class Validation
    {
        public static int ValidateRecordId(string checkId)
        {
            while (!Int32.TryParse(checkId, out _) || Convert.ToInt32(checkId) < 0)
            {
                Console.WriteLine("Please enter the redocd ID. Enter an integer e.g 4");
                checkId = Console.ReadLine();
            }
            int convertedIdInput = Convert.ToInt32(checkId);
            return convertedIdInput;
        }

        public static string ValidateDate(string checkDate)
        {
            while (!DateTime.TryParseExact(checkDate, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Please enter the date in format DD MMMM YYYY e.g 05 May 2006");
                checkDate = Console.ReadLine();

            }
            return checkDate;
        }
        public static string ValidateStartTime(string checkStartTime)
        {
            while (!DateTime.TryParseExact(checkStartTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Enter Start Time in format HH:mm e.g 02:30 which is 2 hours and 30 minutes");
                checkStartTime = Console.ReadLine();

            }
            return checkStartTime;
        }

        public static string ValidateEndTime(string checkEndTime)
        {
            while (!DateTime.TryParseExact(checkEndTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Enter End Time in format HH:mm e.g 02:30 which is 2 hours and 30 minutes");
                checkEndTime = Console.ReadLine();

            }
            return checkEndTime;
        }
    }
}
