using System.Data.SqlClient;
using Library.DB;
using System.Text.RegularExpressions;

namespace Library.Accounts
{
    public class Card
    {
        private readonly IDataBase _database;

        public Card(IDataBase database)
        {
            _database = database;
        }

        public bool IsExistAnotherCard(string card)
        {
            SqlParameter[] parameters = { new SqlParameter("@card", card) };
            return _database.SelectData(SqlQueries.IsExistAnotherCardQuery, parameters).Rows.Count == 1;
        }

        public bool CheckOwnCard(string card, int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@card", card),
                new SqlParameter("@id", id)
            };
            return _database.SelectData(SqlQueries.CheckOwnCardQuery, parameters).Rows.Count == 1;
        }

        public decimal TakeBalanceFromCard(string card, int id)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@card", card)
            };

            var result = _database.SelectData(SqlQueries.TakeBalanceFromCardQuery, parameters);
            if (result.Rows.Count == 1)
            {
                return Convert.ToDecimal(result.Rows[0]["balance"]);
            }
            else
            {
                throw new Exception("Balance not found");
            }
        }

        public void AddTransactionWithCardToHistory(DateTime dateTime, string description, decimal sum, string numCard)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Date", dateTime),
                new SqlParameter("@Description", description),
                new SqlParameter("@sum", sum),
                new SqlParameter("@card", numCard)
            };
            _database.InsertUpdateDeleteData(SqlQueries.AddTransactionWithCardToHistoryQuery, parameters);
        }

        public static bool CheckPinCodeWriting(string pincode)
        {
            string pattern = @"^\d{4}$";
            return Regex.IsMatch(pincode, pattern);
        }

        public void ChangePin(string pincode, string numCard)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@pin", pincode),
                new SqlParameter("@card", numCard)
            };
            _database.InsertUpdateDeleteData(SqlQueries.ChangePinQuery, parameters);
        }
    }
}
