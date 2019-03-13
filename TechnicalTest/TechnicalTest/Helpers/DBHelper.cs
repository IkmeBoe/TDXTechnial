using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using TechnicalTest.Inventory;

namespace TechnicalTest.Helpers
{
    internal class DbHelper
    {
        private readonly string _connectionString;
        public DbHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public DbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal List<T> GetDataFromDatabase<T>(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>(query).ToList();
            }

        }

        internal void AddFileToDatabase(string connectionString, string fileName, Byte[] file)
        {
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
                    
                    cmd.ExecuteScalar();
                    
             
                }
            }
        }

        internal void AddItemToDatabase(Item item, string filename)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("spStoreFileRows", connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    connection.Open();

                    cmd.Parameters.Add("@PartId", SqlDbType.UniqueIdentifier).Value = item.PartId;
                    cmd.Parameters.Add("@PartName", SqlDbType.NVarChar).Value = item.PartName;
                    cmd.Parameters.Add("@PartType", SqlDbType.NVarChar).Value = item.PartType;
                    cmd.Parameters.Add("@PartLength", SqlDbType.NVarChar).Value = item.PartLength;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                    cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = item.DateAdded;
                    cmd.Parameters.Add("@Filename", SqlDbType.NVarChar).Value = filename;


                    cmd.ExecuteNonQuery();


                }
            }
        }
    }
}
