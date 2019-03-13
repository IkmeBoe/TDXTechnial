using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using TechnicalTest.Interface;

namespace TechnicalTest.Log
{
    internal class Log : ILog
    {
        public void InsertLog(string connectionString, LogItem itemToInsert)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                connection.Insert(itemToInsert);
            }
        }

        
    }
}
