using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace dotNetCoreApi
{
    public class DBConnector
    {
        private SqlConnection dbConnection = new SqlConnection();

        //private const string dbConnectionString = "Server=tcp:peopletrackerserver.database.windows.net," +
        //    "1433;" +
        //    "Initial Catalog=People_Tracker;" +
        //    "Persist Security Info=False;" +
        //    "User ID=qperior;" +
        //    "Password=Consultants2018;" +
        //    "MultipleActiveResultSets=False;" +
        //    "Encrypt=True;" +
        //    "TrustServerCertificate=False;" +
        //    "Connection Timeout=5;";

        public bool Connect()
        {
            var dbConnectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_DefaultConnectionString");

            try
            {
                //Maybe cleanup before
                dbConnection.Close();

                dbConnection.ConnectionString = dbConnectionString;
                dbConnection.Open();

                if (dbConnection.State == System.Data.ConnectionState.Open)
                    Debug.WriteLine("Connection to database {0} on server {1} opened successfully",
                        dbConnection.Database, dbConnection.DataSource);
                else
                    Debug.WriteLine("Failed to establish connection to database {0} on server {1}",
                         dbConnection.Database, dbConnection.DataSource);

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public StringContent ExecuteQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                Debug.WriteLine("SQL command not set. Make sure your SQL command is not empty");
                return null;
            }

            if (Object.ReferenceEquals(dbConnection, null))
            {
                Debug.WriteLine("dbConnection not initialized. " +
                    "Run 'Connect()' method at least once before calling any other method");
                return null;
            }

            using (SqlCommand command = new SqlCommand(sql, dbConnection))
            {
                try
                {
                    //default return value
                    string quereyResult = string.Empty;
                    SqlDataReader sqlReader = command.ExecuteReader();

                    sqlReader.Read();
                    IDataRecord dr = sqlReader;
                    string txtResult = dr[0].ToString();
                    StringContent jsonContent = new StringContent(txtResult, Encoding.UTF8, "application/json");

                    return jsonContent;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public bool ExecuteNonQuery(string sql)
        {
            if (String.IsNullOrEmpty(sql))
            {
                Debug.WriteLine("SQL command not set. Make sure your SQL command is not empty");
                return false;
            }

            if (Object.ReferenceEquals(dbConnection, null))
            {
                Debug.WriteLine("dbConnection not initialized. " +
                    "Run 'Connect()' method at least once before calling any other method");
                return false;
            }

            using (SqlCommand command = new SqlCommand(sql, dbConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}