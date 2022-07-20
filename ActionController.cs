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
        public static void Add(CodingSession session)
        {
            //string date = UserInput.GetDateInput();
            //string start = UserInput.GetStartTime();
            //string end = UserInput.GetEndTime();
            //string duration = UserInput.DurationCalculator(start, end);
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        $"INSERT INTO TrackerDatabase (Date, StartTime, EndTime, Duration) VALUES('{session.Date}','{session.StartTime}','{session.EndTime}','{session.Duration}')";
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
                                Date = reader.GetString(1),

                                StartTime = reader.GetString(2),
                                EndTime = reader.GetString(3),
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
        public static void Remove(int recordId)
        {
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        $"DELETE from TrackerDatabase WHERE Id = '{recordId}'";

                    int rowCount = tableCmd.ExecuteNonQuery();
                    if (rowCount == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"*****Record with id {recordId} doesn't exist*****");
                       
                    }
                    else
                    {
                        Console.WriteLine("Record has been deleted");
                    }

                }

            }
        }
        public static void UpdateCheck(int recordId)
        {

            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                var tableCmd = connection.CreateCommand();

                connection.Open();

                tableCmd.CommandText =
                    $"SELECT * from TrackerDatabase WHERE Id = {recordId}";

                int checkQuery = Convert.ToInt32(tableCmd.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine("Record Id doesn't exist");
                    connection.Close();
                }

                


            }
        }
        public static void Update(CodingSession session)
        {

            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {

                var tableCmd = connection.CreateCommand();

                connection.Open();

                tableCmd.CommandText =
                    $"SELECT * from TrackerDatabase WHERE Id = {session.Id}";


                tableCmd.CommandText =
                    $"UPDATE TrackerDatabase SET Date = '{session.Date}', StartTime = '{session.StartTime}' , EndTime = '{session.EndTime}', Duration = '{session.Duration}' WHERE ID = {session.Id}";

                tableCmd.ExecuteNonQuery();
                connection.Close();



            }
        }
    }
}
