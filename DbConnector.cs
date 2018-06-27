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

        public string ExecuteQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                Debug.WriteLine("SQL command not set. Make sure your SQL command is not empty");
                return "Query Error";
            }

            if (Object.ReferenceEquals(dbConnection, null))
            {
                Debug.WriteLine("dbConnection not initialized. " +
                    "Run 'Connect()' method at least once before calling any other method");
                return "Query Error";
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
                    return txtResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Query Error";
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