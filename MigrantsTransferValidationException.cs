using System;
using MigrantsTransferTrackerDAL;

namespace MigrantsTransferTrackerBLL
{
    public class MigrantsTransferValidationException : Exception
    {
        public MigrantsTransferValidationException(string message) : base(message)
        {
            //Console.WriteLine(message);
        }
    }
}

