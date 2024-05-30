using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}