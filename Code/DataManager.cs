using System;
using Microsoft.Data.SqlClient; /*I was getting a warning with the SqlConnection below and consulted
                                ChatGPT, it guided me to use this namespace instead as per what the warning recommended */


namespace Console_Application
{
    public class DataManager
    {
        private const string ConnectionString =
            "Server=(localdb)\\ProjectModels;Database=Video_Games;Trusted_Connection=True;TrustServerCertificate=True;";
        
        public DataManager()
        {
            
        }

        public void FetchData() //Case 1: Get Data
        {
            Console.WriteLine("Fetch Data successfully called");

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Successfully connected.");
                }
            }

            catch (SqlException)
            {
                Console.WriteLine("Connection Failed. Returning to options.");
                return;
            }

            Console.WriteLine("Closed Connection.");
        }

        public void AddData() //Case 2: Add Data
        {
            Console.WriteLine("Add Data successfully called");
        }

        public void UpdateData() //Case 3: Update Data
        {
            Console.WriteLine("Update Data successfully called");
        }
        
        public void DeleteData() //Case 4: Delete Data
        {
            Console.WriteLine("Delete Data successfully called");
        }
    }
}