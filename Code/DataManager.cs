using System;
using Microsoft.Data.SqlClient; /*I was getting a warning with the SqlConnection below and consulted
                                ChatGPT, it guided me to use this namespace instead as per what the warning recommended */


namespace Console_Application
{
    public class DataManager
    {
        private String sql = ""; //Declaring connection string
        private int choice = 0; //Default choice value, guarantees the user must be prompted for choice of table to access

        private const string ConnectionString = //Constant connection string, hopefully to minimize errors
            "Server=(localdb)\\ProjectModels;Database=Video_Games;Trusted_Connection=True;TrustServerCertificate=True;";

        public void DisplayData(int tempChoice) //This handles the actual displaying of data pulled from a table
        {
            switch (tempChoice)
            {
                case 1:
                    sql = "SELECT FranchiseID, Franchise FROM dbo.Franchises;";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                var franchiseId = reader["FranchiseID"];
                                var franchise = reader["Franchise"];

                                Console.WriteLine($"{franchiseId}: {franchise}");
                            }
                        }
                    }

                    catch (SqlException)
                    {
                        Console.WriteLine("Connection Failed. Returning to options.");
                        return;
                    }

                    break;

                case 2:
                    sql = "SELECT t.GameID, t.GameTitle, t.Console, f.Franchise FROM dbo.Titles AS t INNER JOIN dbo.Franchises AS f ON t.FranchiseID = f.FranchiseID;"; //I did have ChatGPT generate this because I wasn't sure what else to do

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                var gameID = reader["GameID"];
                                var gameTitle = reader["GameTitle"];
                                var franchise = reader["franchise"];
                                var console = reader["Console"];

                                Console.WriteLine($"{gameID}: {gameTitle} - {console} - Franchise: {franchise}");
                            }
                        }
                    }

                    catch (SqlException)
                    {
                        Console.WriteLine("Connection Failed. Returning to options.");
                        return;
                    }

                    break;

                case 998855: //Random number string to hopefully prevent this case from being randomly chosen
                    sql = "SELECT DISTINCT Console FROM dbo.Titles ORDER BY Console;";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            SqlDataReader reader = command.ExecuteReader();

                            int i = 1;

                            while (reader.Read())
                            {
                                var tableConsole = reader["Console"];

                                Console.WriteLine(i + ". " + tableConsole);

                                i++;
                            }
                        }
                    }

                    catch (SqlException)
                    {
                        Console.WriteLine("Connection Failed. Returning to options.");
                        return;
                    }

                    break;
            }
        }

        public void FetchData() //Case 1: Get Data, This method is to correctly select a specific table and change the value of choice
        {                       
            Console.WriteLine("Which table would you like to access? Enter the corresponding number.");
            Console.WriteLine("Options: \n" +
                              "1. Franchises \n" +
                              "2. Titles \n");

            String choiceInput = Console.ReadLine();

            bool inputCheck = int.TryParse(choiceInput, out choice);

            while (inputCheck == false || (choice != 1 && choice != 2))
            {
                Console.WriteLine("\nPlease input a valid number.");
                choiceInput = Console.ReadLine();

                inputCheck = int.TryParse(choiceInput, out choice);
            }
        }

        public void AddData() //Case 2: Add Data
        {
            FetchData();

            switch(choice)
            {
                case 1:
                    Console.WriteLine("What Franchise would you like to add?");
                    String franchise = Console.ReadLine();

                    sql = "INSERT INTO dbo.Franchises (Franchise) VALUES (@franchise);";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            command.Parameters.AddWithValue("@franchise", franchise);

                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("Successfully Inserted.");

                        DisplayData(choice);
                    }

                    catch (SqlException ex)
                    {
                        Console.WriteLine("Database error: " + ex.Message);
                        return;
                    }

                    break;

                case 2:
                    Console.WriteLine("What is the title of the Game you would you like to add?"); //Game Title Input
                    String gameName = Console.ReadLine();

                    Console.WriteLine("What console did this game release on? Enter name present below or type 'New'. Be sure to check spelling."); //Console Input
                    Console.WriteLine("Here are the currently stored consoles:\n");
                    DisplayData(998855);

                    string consoleInput = Console.ReadLine();

                    if (consoleInput.Equals("NEW", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("What new console would you like to add?");
                        consoleInput = Console.ReadLine();

                        // Check whether the "new" console already exists,
                        // ignoring capitalization.
                        if (TryGetMatchingConsole(consoleInput, out String matchedConsole))
                        {
                            // It already exists, so use its database capitalization.
                            consoleInput = matchedConsole;
                        }

                        // If it does NOT already exist, consoleInput stays as the
                        // new console name the user entered.
                    }

                    else if (TryGetMatchingConsole(consoleInput, out String matchedConsole))
                    {
                        // The user entered an existing console directly.
                        // Replace their capitalization with the database version.
                        consoleInput = matchedConsole;
                    }

                    else
                    {
                        Console.WriteLine("That console does not exist. Type NEW if you want to add one.");
                        return;
                    }

                    Console.WriteLine("\nFinally, what franchise does this game belong to?");
                    Console.WriteLine("Enter the number corresponding to a listed franchise:");

                    DisplayData(1);
                    Console.WriteLine();

                    String franchiseInput = Console.ReadLine();

                    bool validNumber = int.TryParse(franchiseInput, out int franchiseId);

                    if (!validNumber || !FranchiseIdExists(franchiseId))
                    {
                        Console.WriteLine("That is not a valid Franchise ID.");
                        Console.WriteLine("If this is a new franchise, add it in a separate insert first.");
                        return;
                    }

                    sql = "INSERT INTO dbo.Titles (GameTitle, FranchiseID, [Console]) " + "VALUES (@gameName, @franchiseId, @consoleInput);";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            command.Parameters.AddWithValue("@gameName", gameName);
                            command.Parameters.AddWithValue("@franchiseId", franchiseId);
                            command.Parameters.AddWithValue("@consoleInput", consoleInput);

                            command.ExecuteNonQuery();
                        }

                        Console.WriteLine("\nSuccessfully Inserted\n");
                        DisplayData(2);
                    }

                    catch (SqlException ex)
                    {
                        Console.WriteLine("Database error: " + ex.Message);
                        return;
                    }

                    break;
            }
        }

        public void UpdateData() //Case 3: Update Data
        {
            #warning TODO: Create UpdateData()
        }
        
        public void DeleteData() //Case 4: Delete Data
        {
            #warning TODO: Create DeleteData()
        }

        public int GetChoice()
        {
            return choice;
        }

        private bool TryGetMatchingConsole(string userInput, out string console)
        {
            console = "";

            sql = "SELECT DISTINCT Console FROM dbo.Titles ORDER BY Console;";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var tableConsole = reader["Console"].ToString();

                        if (string.Equals(userInput, tableConsole,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            // This saves the correctly capitalized database value.
                            console = tableConsole;
                        }
                    }
                }

                return console != "";
            }

            catch (SqlException)
            {
                Console.WriteLine("Connection Failed. Returning to options.");
                return false;
            }
        }

        private bool FranchiseIdExists(int franchiseId)
        {
            sql = "SELECT COUNT(*) FROM dbo.Franchises WHERE FranchiseID = @franchiseId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@franchiseId", franchiseId);

                    int numberOfMatches = (int)command.ExecuteScalar();

                    return numberOfMatches > 0;
                }
            }

            catch (SqlException)
            {
                Console.WriteLine("Connection Failed. Returning to options.");
                return false;
            }
        }
    }
}