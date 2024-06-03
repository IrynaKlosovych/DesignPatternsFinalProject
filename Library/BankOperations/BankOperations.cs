using Library.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Accounts;

namespace Library.BankOperations
{
    public static class BankOperations
    {
        public static void UpdateMoney(DataBase instance, string numberCard, decimal sum)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@newBalance", sum),
            new SqlParameter("@card", numberCard)
            };
            instance.InsertUpdateDeleteData(SqlQueries.UpdateMoneyQuery, parameters);
        }
        public static Dictionary<string, decimal> CheckBalanceOnAllCards(DataBase instance, int idUser)
        {
            Dictionary<string, decimal> cardBalance = new Dictionary<string, decimal>();

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", idUser)
            };

            DataTable result = instance.SelectData(SqlQueries.CheckBalanceOnAllCardsQuery, parameters);
            if (result.Rows.Count >0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string card = row["card_number"].ToString()!;
                    decimal balance = Convert.ToDecimal(row["balance"]);
                    cardBalance[card] = balance;
                }
                return cardBalance;
            }
            else
            {
                throw new Exception();
            }
        }
        
        public static DataTable ShowHistory(DataBase instance, int userID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", userID)
            };

            DataTable result = instance.SelectData(SqlQueries.ShowHistoryQuery, parameters);
            return result;
        }
    }
}
