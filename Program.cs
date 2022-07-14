using Microsoft.Data.Sqlite;
using System;
using System.Configuration;

namespace CodingTracker2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                //Creating the command that will be sent to the database
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    //Declaring what is that command (in SQL syntax)
                    tableCmd.CommandText =
                        @"CREATE TABLE IF NOT EXISTS TrackerDatabase (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT unique,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                    )";

                    // Executing the command, which isn't a query, it's not asking to return data from the database.
                    tableCmd.ExecuteNonQuery();
                }
                // We don't need to close the connection or the command. The 'using statement' does that for us.
            }

            /* Once we check if the database exists and create it (or not),
            we will call the next method, which will handle the user's input. Your next step is to create this method*/
            //GetUserInput();
            CodingController.MainMenu();
        }
    }
    
}
