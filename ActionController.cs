using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using ConsoleTableExt;

namespace CodingTracker2
{
    class ActionController
    {
        public static void CreateEntry()
        {
            string date = UserInput.GetDateInput();
            string start = UserInput.GetStartTime();
            string end = UserInput.GetEndTime();
            string duration = UserInput.DurationCalculator(start, end);
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        $"INSERT INTO TrackerDatabase (Date, StartTime, EndTime, Duration) VALUES('{date}','{start}','{end}','{duration}')";
                    try
                    {

                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("UNIQUE"))
                        {
                            Console.WriteLine("A record for this date already exists.");
                        }

                    }
                    connection.Close();
                }
                
            }
        }
        public static void DisplayAll()
        {

            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                var tableCmd = connection.CreateCommand();
                connection.Open();
                tableCmd.CommandText =
                    $"SELECT * FROM TrackerDatabase";

                List<CodingSession> tableData = new List<CodingSession>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new CodingSession
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd MMMM yyyy", CultureInfo.InvariantCulture),

                                StartTime = DateTime.ParseExact(reader.GetString(2), "HH:mm", CultureInfo.InvariantCulture),
                                EndTime = DateTime.ParseExact(reader.GetString(3), "HH:mm", CultureInfo.InvariantCulture),
                                Duration = reader.GetString(4)
                            });
                    }

                }
                else
                {
                    Console.WriteLine("No entries found");
                }

                connection.Close();

                ConsoleTableBuilder
                    .From(tableData)
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine(TableAligntment.Center);

                foreach (var item in tableData)
                {
                    Console.WriteLine($"Id: {item.Id} Date:{item.Date.ToString("dd MMMM yyyy")} StartTime:{item.StartTime.ToString("HH:mm")} EndTime:{item.EndTime.ToString("HH:mm")} Duration: {item.Duration}");

                }



            }
        }
    }
}
