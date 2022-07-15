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

                //foreach (var item in tableData)
               //{
                   // Console.WriteLine($"Id: {item.Id} Date:{item.Date.ToString("dd MMMM yyyy")} StartTime:{item.StartTime.ToString("HH:mm")} EndTime:{item.EndTime.ToString("HH:mm")} Duration: {item.Duration}");

                //} 
            }
        }
        public static void Delete()
        {
            Console.Clear();
            DisplayAll();
            var recordID = UserInput.GetRecordId("Please enter the record Id you want to delete or enter 0 to return to main menu.");
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        $"DELETE from TrackerDatabase WHERE Id = '{recordID}'";

                    int rowCount = tableCmd.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        Console.WriteLine($"Record with id {recordID} doesn't exist");
                        Delete();
                    }
                    else
                    {
                        Console.WriteLine("Record has been deleted");
                    }

                }

            }
        }
        public static void Update()
        {

            DisplayAll();
            int recordID = UserInput.GetRecordId("Please enter the record Id you want to update or enter 0 to return to main menu.");
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                var tableCmd = connection.CreateCommand();

                connection.Open();

                tableCmd.CommandText =
                    $"SELECT * from TrackerDatabase WHERE Id = {recordID}";

                int checkQuery = Convert.ToInt32(tableCmd.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine("Record Id doesn't exist");
                    connection.Close();
                    Update();
                }

                string date = UserInput.GetDateInput();
                string start = UserInput.GetStartTime();
                string end = UserInput.GetEndTime();
                string duration = UserInput.DurationCalculator(start, end);
                tableCmd.CommandText =
                    $"UPDATE TrackerDatabase SET Date = '{date}', StartTime = '{start}' , EndTime = '{end}', Duration = '{duration}' WHERE ID = {recordID}";

                tableCmd.ExecuteNonQuery();
                connection.Close();



            }
        }
    }
}
