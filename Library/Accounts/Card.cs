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

        public static void AddTransactionWithCardToHistory(DataBase instance, DateTime dateTime, string description, decimal sum, string numCard)
        {
            string query = "insert into History(date, description, suma, id_user, id_card) values(@Date, @Description, @sum, (select u.id_user from MyUser u inner join CARD c on u.id_user=c.id_user where card_number= @card), (select id_card from CARD  where card_number= @card))";
            SqlParameter[] parameters = new SqlParameter[]
           {
            new SqlParameter("@Date", dateTime),
            new SqlParameter("@Description", description),
            new SqlParameter("@sum", sum),
            new SqlParameter("@card", numCard)
           };
            instance.InsertUpdateDeleteData(query, parameters);
        }
        public static bool CheckPinCodeWriting(string pincode)
        {
            string pattern = @"^\d{4}$";
            return Regex.IsMatch(pincode, pattern);
        }

        public static void ChangePin(DataBase instance, string pincode, string numCard)
        {
            string query = "update CARD set pincode = @pin where card_number=@card";
            SqlParameter[] parameters = new SqlParameter[]
           {
            new SqlParameter("@pin", pincode),
            new SqlParameter("@card", numCard)
           };
            instance.InsertUpdateDeleteData(query, parameters);
        }
    }
}
