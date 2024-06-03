using Library.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Accounts
{
    public static class Phone
    {
        public static bool IsValidMobilePhoneNumber(string phoneNumber)
        {
            string pattern = @"^0(39|50|6[3|6-8]|73|89|9[1-9])\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static class DatabaseHelper
        {
            public static bool Exists(DataBase instance, string query, SqlParameter[] parameters)
            {
                DataTable result = instance.SelectData(query, parameters);
                return result.Rows.Count == 1;
            }
        }

        public static bool IsExistPhoneInDB(DataBase instance, string phoneNumber)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@PhoneNumber", phoneNumber)
            };

            return DatabaseHelper.Exists(instance, SqlQueries.IsExistPhoneInDBQuery, parameters);
        }

        public static bool CheckPhone(DataBase instance, string phoneNumber)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@PhoneNumber", phoneNumber)
            };

            return DatabaseHelper.Exists(instance, SqlQueries.CheckPhoneQuery, parameters);
        }

        public static void AddMoneyToPhone(DataBase instance, string phoneNumber, decimal sum)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@sum", sum),
            new SqlParameter("@phone", phoneNumber)
            };
            instance.InsertUpdateDeleteData(SqlQueries.AddMoneyToPhoneQuery, parameters);
        }
    }
}
