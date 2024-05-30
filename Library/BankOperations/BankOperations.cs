using Library.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations
{
    public static class BankOperations
    {
        private static void AddMoney(string numberCard, decimal sum)
        {

        }
        private static void WithdrawMoney(string numberCard, decimal sum)
        {

        }
        public static Dictionary<string, decimal> CheckBalanceOnAllCards(DataBase instance, int idUser)
        {
            Dictionary<string, decimal> cardBalance = new Dictionary<string, decimal>();

            string query = "select card_number, balance from CARD c inner join MyUser u on c.id_user=u.id_user where u.id_user = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", idUser)
            };

            DataTable result = instance.SelectData(query, parameters);
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
        
    }
}
