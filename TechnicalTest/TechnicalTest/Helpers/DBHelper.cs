using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
namespace TechnicalTest.Helpers
{
    internal class DbHelper
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        internal List<T> GetDataFromDatabase<T>(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>(query).ToList();
            }

        }
        internal string AddFileToDatabase(string connectionString, string fileName, Byte[] file)
        {
            string itemGuid = string.Empty;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("spStoreFileDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    connection.Open();
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = fileName.Split('\\').Last();
                    cmd.Parameters.Add("@FileStream", SqlDbType.VarBinary).Value = file;
                    cmd.Parameters.Add("@FileDate",SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@User", SqlDbType.NVarChar).Value = Environment.UserName;

                    SqlParameter p = new SqlParameter("@itemGuid", SqlDbType.UniqueIdentifier);
                    p.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p);
                    itemGuid = (string) cmd.Parameters["@itemGuid"].Value;
                     cmd.ExecuteNonQuery();
                }
            }

            return itemGuid;
        }
    }
}
