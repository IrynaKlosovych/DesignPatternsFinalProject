using Library.DB;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Library.Accounts
{
    public class Phone
    {
        private readonly IDataBase _database;

        public Phone(IDataBase database)
        {
            _database = database;
        }

        public static bool IsValidMobilePhoneNumber(string phoneNumber)
        {
            string pattern = @"^0(39|50|6[3|6-8]|73|89|9[1-9])\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public bool IsExistPhoneInDB(string phoneNumber)
        {
            SqlParameter[] parameters = { new SqlParameter("@PhoneNumber", phoneNumber) };
            return _database.SelectData(SqlQueries.IsExistPhoneInDBQuery, parameters).Rows.Count == 1;
        }

        public bool CheckPhone(string phoneNumber)
        {
            SqlParameter[] parameters = { new SqlParameter("@PhoneNumber", phoneNumber) };
            return _database.SelectData(SqlQueries.CheckPhoneQuery, parameters).Rows.Count == 1;
        }

        public void AddMoneyToPhone(string phoneNumber, decimal sum)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@sum", sum),
                new SqlParameter("@phone", phoneNumber)
            };
            _database.InsertUpdateDeleteData(SqlQueries.AddMoneyToPhoneQuery, parameters);
        }

    }
}
