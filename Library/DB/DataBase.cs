using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Library.DB
{
    public sealed class DataBase
    {
        private static DataBase? _instance;
        private static object _refObj = new object();
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection connection;

        public static DataBase GetInstance()
        {
            if (DataBase._instance == null)
            {
                lock (DataBase._refObj)
                {
                    if (DataBase._instance == null)
                    {
                        DataBase._instance = new DataBase();
                    }
                }
            }
            return DataBase._instance;
        }

        private DataBase()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public DataTable SelectData(string sqlQuery, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        public void InsertUpdateDeleteData(string query, SqlParameter[]? parameters = null)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}