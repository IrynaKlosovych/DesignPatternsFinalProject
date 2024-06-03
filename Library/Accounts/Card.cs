using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DB;
using System.Text.RegularExpressions;

namespace Library.Accounts
{
    public class Card
    {
        public static class DatabaseHelper
        {
            public static bool Exists(DataBase instance, string query, SqlParameter[] parameters)
            {
                DataTable result = instance.SelectData(query, parameters);
                if (result.Rows.Count == 1)
                {
                    return true;
                }
                return false;
            }
        }
        public static bool IsExistAnotherCard(DataBase instance, string card)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@card", card)
            };

            return DatabaseHelper.Exists(instance, SqlQueries.IsExistAnotherCardQuery, parameters);
        }

        public static bool CheckOwnCard(DataBase instance, string card, int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@card", card),
                new SqlParameter("@id", id)
            };

            return DatabaseHelper.Exists(instance, SqlQueries.CheckOwnCardQuery, parameters);
        }

        public static decimal TakeBalanceFromCard(DataBase instance, string card, int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", id),
            new SqlParameter("@card", card)
            };

            DataTable result = instance.SelectData(SqlQueries.TakeBalanceFromCardQuery, parameters);
            if (result.Rows.Count == 1)
            {
                decimal balance = Convert.ToDecimal(result.Rows[0]["balance"]);
                return balance;
            }
            else
            {
                throw new Exception();
            }
        }

        public static void AddTransactionWithCardToHistory(DataBase instance, DateTime dateTime, string description, decimal sum, string numCard)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", dateTime),
                new SqlParameter("@Description", description),
                new SqlParameter("@sum", sum),
                new SqlParameter("@card", numCard)
            };
            instance.InsertUpdateDeleteData(SqlQueries.AddTransactionWithCardToHistoryQuery, parameters);
        }
        public static bool CheckPinCodeWriting(string pincode)
        {
            string pattern = @"^\d{4}$";
            return Regex.IsMatch(pincode, pattern);
        }

        public static void ChangePin(DataBase instance, string pincode, string numCard)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@pin", pincode),
                new SqlParameter("@card", numCard)
            };
            instance.InsertUpdateDeleteData(SqlQueries.ChangePinQuery, parameters);
        }
    }
}
