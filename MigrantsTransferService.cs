using MigrantsTransferTrackerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MigrantsTransferTrackerBLL
{
    public class MigrantsTransferService
    {
        public MigrantsTransferDAO migrantsTransferDAO;

        public MigrantsTransferService()
        {
            
        }

        public bool Validate(MigrantsTransfer migrantsTransfer, out string validationError)
        {
            bool IsValid = false;
            validationError = "";
            
            if (migrantsTransfer.TransferDate > DateTime.Now)
            {
                validationError = "TransferDate cannot be a future date";
                return IsValid;
            }
            if (string.IsNullOrEmpty(migrantsTransfer.FromState))
            {
                validationError = "FromState is required";
                return IsValid;
            }

            if (string.IsNullOrEmpty(migrantsTransfer.ToState))
            {
                validationError = "ToState is required";
                return IsValid;
            }

            if (migrantsTransfer.FromState == migrantsTransfer.ToState)
            {
                validationError = "FromState and ToState cannot be same";
                return IsValid;
            }
            if (migrantsTransfer.NoOfMigrantsTransfered < 0)
            {
                validationError = "Invalid value for NoOfMigrantsTransferred";
                return IsValid;
            }

            IsValid = true;
            return IsValid;
        }

        public bool AddNewTransfer(MigrantsTransfer migrantsTransfer)
        {
            bool IsAdded = false;

            string validationError = "";
            if (Validate(migrantsTransfer,out validationError) == true)
            {
                migrantsTransferDAO = new MigrantsTransferDAO();
 
                int rowsAdded=migrantsTransferDAO.AddTransferDetails(migrantsTransfer.TransferDate,
                                      migrantsTransfer.FromState,
                                      migrantsTransfer.ToState,
                                      migrantsTransfer.NoOfMigrantsTransfered,
                                      migrantsTransfer.TransferMode,
                                      migrantsTransfer.VehicleDetails);
                if (rowsAdded > 0)
                {
                    Console.WriteLine("Transfer details saved successfully");
                    Console.WriteLine("{0} rows added.", rowsAdded);
                }
            }

            else
            {
                throw new MigrantsTransferValidationException(validationError);
            }

            return IsAdded;
        }

        public Dictionary<string, long> GetTransferHistory(string fromState, DateTime fromDate, DateTime toDate)
        {
            
            Dictionary<string, long> TransferHistory = new Dictionary<string, long>();

            try
            {
                MigrantsTransfer migrantsTransfer = new MigrantsTransfer();
                migrantsTransferDAO = new MigrantsTransferDAO();
                DataTable table = migrantsTransferDAO.GetTransferHistory(fromState, fromDate, toDate);
                //var row = table.NewRow();
                //if (table != null)
                //{
                    //if (table.Rows.Count > 0)
                    //{
                        //do your code 
                    //}
                
                    foreach (DataRow row in table.Rows)
                    TransferHistory.Add((string)row[0], Convert.ToInt64(row[1]));

                    foreach (KeyValuePair<string, long> entry in TransferHistory)
                    {

                        Console.WriteLine("{0} migrants transferred to {1}", entry.Value, entry.Key);
                        // do something with entry.Value or entry.Key
                    }
                //}

                //else
                    //Console.WriteLine("No migratiosn found for {0} between {1} and {2}", fromState, fromDate, toDate);
                //Do not change method signature
            }

            catch (Exception)
            {
                //Handle your exeption
                throw;
            }

            return TransferHistory;
        }
    }
}
