using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace MigrantsTransferTrackerDAL
{
    public class MigrantsTransferDAO
    {
        //Create public fields here
        public SqlConnection connection;
        public SqlCommand command;
        public SqlDataAdapter adapter;

        public MigrantsTransferDAO()
        {
            string con = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            connection = new SqlConnection(con);
        }

        public int AddTransferDetails(DateTime transferDate,
                                      string fromState,
                                      string toState,
                                      int noOfMigrantsTransfered,
                                      string transferMode,
                                      string vehicleDetails)
        {
            int RowsAdded = 0;
            //Console.WriteLine("add transfer working");

            try
            {
                string querystring = "insert into MigrantsTransfer values('" + transferDate + "','" + fromState + "','" + toState + "'," + noOfMigrantsTransfered + ",'" + transferMode + "','" + vehicleDetails + "')";

                connection.Open();

                Console.WriteLine("connection established");
                command = new SqlCommand(querystring, connection);

                //SqlDataReader reader = migrantsTransferDAO.command.ExecuteReader();
                RowsAdded = command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("connection closed");
            }
            catch (Exception e)
            {
                throw;
            }

            return RowsAdded;
        }

        public DataTable GetTransferHistory(string fromState, DateTime fromDate, DateTime toDate)
        {
            DataTable table = new DataTable();
            
            string con = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;

            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();

                    string querystring = "Select ToState, sum(MigrantsTransfered) as NoOfMigrants from MigrantsTransfer where TransferDate between '" + fromDate + "' and '" + toDate + "' and FromState='" + fromState + "' group by ToState";
                    command = new SqlCommand(querystring, connection);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                    connection.Close();

                    //Console.WriteLine("datatable created");
                    if (table.Rows.Count <= 0)
                    {
                        //do your code
                        Console.WriteLine("No migrations found for {0} between {1:MM/dd/yyyy} and {2:MM/dd/yyyy}", fromState, fromDate, toDate);
                    }
                }

            }

            catch (Exception )
            {
                //Handle your exeption
                throw;
            }

            return table;
        }
    }
}


