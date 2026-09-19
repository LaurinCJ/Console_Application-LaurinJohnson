using System;
using Microsoft.Data.SqlClient; /*I was getting a warning with the SqlConnection below and consulted
                                ChatGPT, it guided me to use this namespace instead as per what the warning recommended */


namespace Console_Application
{
    public class DataManager
    {
        String sql = "";
        int choice = 0;
        private const string ConnectionString =
            "Server=(localdb)\\ProjectModels;Database=Video_Games;Trusted_Connection=True;TrustServerCertificate=True;";
        
        public DataManager()
        {
            
        }

        public void DisplayData()
        {
            switch (GetChoice())
            {
                case 1:
                    sql = "SELECT FranchiseID, Franchise FROM dbo.Franchises;";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sql, connection);

                            Console.WriteLine("Successfully connected.");

                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                var franchiseId = reader["FranchiseID"];
                                var franchise = reader["Franchise"];

                                Console.WriteLine($"{franchiseId}: {franchise}");
                            }
                        }
                        Console.WriteLine("\nClosed Connection.");
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

                            Console.WriteLine("Successfully connected.");

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
                        Console.WriteLine("\nClosed Connection.");
                    }

                    catch (SqlException)
                    {
                        Console.WriteLine("Connection Failed. Returning to options.");
                        return;
                    }

                    break;
            }
        }

        public int FetchData() //Case 1: Get Data
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

            return choice;
        }

        public void AddData() //Case 2: Add Data
        {
            FetchData();
        }

        public void UpdateData() //Case 3: Update Data
        {
            Console.WriteLine("Update Data successfully called");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected.");


                }
                Console.WriteLine("\nClosed Connection.");
            }

            catch (SqlException)
            {
                Console.WriteLine("Connection Failed. Returning to options.");
                return;
            }
        }
        
        public void DeleteData() //Case 4: Delete Data
        {
            Console.WriteLine("Delete Data successfully called");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected.");


                }
                Console.WriteLine("\nClosed Connection.");
            }

            catch (SqlException)
            {
                Console.WriteLine("Connection Failed. Returning to options.");
                return;
            }
        }

        public int GetChoice()
        {
            return choice;
        }
    }
}