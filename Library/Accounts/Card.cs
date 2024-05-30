using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DB;

namespace Library.Accounts
{
    public class Card
    {
        public static bool IsExistAnotherCard(DataBase instance, string card)
        {
            string query = "select id_card from CARD where card_number = @card";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@card", card)
            };

            DataTable result = instance.SelectData(query, parameters);
            if (result.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }

        public static bool CheckOwnCard(DataBase instance, string card, int id)
        {
            string query = "select id_card from CARD c inner join MyUser u on c.id_user = u.id_user where card_number = @card and u.id_user=@id";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@card", card),
            new SqlParameter("@id", id)
            };

            DataTable result = instance.SelectData(query, parameters);
            if (result.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }

        public static decimal TakeBalanceFromCard(DataBase instance, string card, int id)
        {
            string query = "select balance from CARD c inner join MyUser u on c.id_user=u.id_user where u.id_user = @id and card_number = @card";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@id", id),
            new SqlParameter("@card", card)
            };

            DataTable result = instance.SelectData(query, parameters);
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
    }
}
