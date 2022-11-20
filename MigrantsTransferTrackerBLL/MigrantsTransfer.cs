using System;
using System.Collections.Generic;
using MigrantsTransferTrackerDAL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrantsTransferTrackerBLL
{
    public class MigrantsTransfer
    {
        public DateTime TransferDate { get; set; }
        public string FromState { get; set; }
        public string ToState { get; set; }
        public int NoOfMigrantsTransfered { get; set; }
        public string TransferMode { get; set; }
        public string VehicleDetails { get; set; }

        public MigrantsTransfer()
        {
            //code
        }

        public MigrantsTransfer(DateTime transferDate,string fromState,
                                string toState, int noOfMigrantsTransferred,
                                string transferMode, string vehicleDetails)
        {
            TransferDate = transferDate;
            FromState=fromState;
            ToState=toState;
            NoOfMigrantsTransfered = noOfMigrantsTransferred;
            TransferMode = transferMode;
            VehicleDetails = vehicleDetails;
        }
    }
}
