using Microsoft.VisualBasic;
using MigrantsTransferTrackerBLL;
using System;
using System.Globalization;

namespace MigrantsTransferTrackerConsoleUi   //DO NOT change the namespace name
{
    class Program     //DO NOT change the class name
    {
        public static void Main(string[] args)
        {
            int choice;
            var migrantsTransferService = new MigrantsTransferService();
            do
            {
                Console.WriteLine("Available Operations");
                Console.WriteLine("1. Add migrants transfers data");
                Console.WriteLine("2. Show data by state");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter your choice:");
                choice = Int32.Parse(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            CollectData(migrantsTransferService);
                            break;
                        case 2:
                            DisplayTransferHistory(migrantsTransferService);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (MigrantsTransferValidationException ex)
                {
                    Console.WriteLine("Validation error : " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                }
            } while (choice != 3);
        }

        private static void CollectData(MigrantsTransferService service)
        {
            MigrantsTransfer migrantsTransfer = new MigrantsTransfer();

            Console.WriteLine("Enter transfer date in MM/dd/yyyy format");
            migrantsTransfer.TransferDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture); ;

            Console.WriteLine("Enter the name of state from where migrants are transferred");
            migrantsTransfer.FromState = Console.ReadLine();

            Console.WriteLine("Enter the name of state to where migrants are transferred");
            migrantsTransfer.ToState = Console.ReadLine(); ;

            Console.WriteLine("Enter the total number of migrants being transferred");
            migrantsTransfer.NoOfMigrantsTransfered = Int32.Parse(Console.ReadLine()); ;

            Console.WriteLine("Enter the mode of transferred being used");
            migrantsTransfer.TransferMode = Console.ReadLine();

            Console.WriteLine("Enter the vehicle number used for transfer");
            migrantsTransfer.VehicleDetails = Console.ReadLine(); ;

            //validating and saving entered data to database

            service.AddNewTransfer(migrantsTransfer);
        }

        private static void DisplayTransferHistory(MigrantsTransferService service)
        {
            Console.WriteLine("Enter the name of state whose migration report required");
            string stateName = Console.ReadLine();
            
            Console.WriteLine("Enter the start date for the report in MM/dd/yyyy format");
            DateTime fromDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            
            Console.WriteLine("Enter the end date for the report in MM/dd/yyyy format");
            DateTime toDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            
            service.GetTransferHistory(stateName, fromDate, toDate);
        }
    }
}

