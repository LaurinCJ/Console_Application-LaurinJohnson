using System;
using System.ComponentModel;

namespace Console_Application
{
    public class ConsoleApp
    {
        static void Main(string[] args)
        {
            DataManager DataManage = new DataManager();
            bool continueRunning = true;
            Console.WriteLine("Welcome to the Video Game Database!");

            do
            {
                Console.WriteLine("Enter the number of the option you would like to select.");
                Console.Write("1. Get Data \n" +    //I'm not sure why it wont just let me skip lines but 
                              "2. Add Data \n" +    //keep things in quotes, I assume this is how it will
                              "3. Update Data \n" + //have to be formatted
                              "4. Delete Data \n" +
                              "5. Exit \n \n");

                String choiceInput = Console.ReadLine();

                bool inputCheck = int.TryParse(choiceInput, out int choice);

                while (inputCheck == false || choice < 1 || choice > 5)     //I elected to use this instead of a try-catch
                {                                                           //exception due to the conditions being checked
                    Console.WriteLine("\n Please input a valid number.");
                    choiceInput = Console.ReadLine();

                    inputCheck = int.TryParse(choiceInput, out choice);
                }

                switch (choice)
                {
                    case 1:
                        DataManage.FetchData();

                        Console.WriteLine();
                        break;

                    case 2:
                        DataManage.AddData();

                        Console.WriteLine();
                        break;

                    case 3:
                        DataManage.UpdateData();

                        Console.WriteLine();
                        break;

                    case 4:
                        DataManage.DeleteData();

                        Console.WriteLine();
                        break;

                    case 5:
                        continueRunning = false;
                        break;
                }
            }
            while(continueRunning);

            Console.WriteLine("Goodbye!");
        }
    }
}