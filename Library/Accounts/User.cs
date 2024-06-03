using Library.DB;
using System.Data;
using System.Data.SqlClient;

namespace Library.Accounts
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string PhoneNumber { get; private set; }

        private User(int id, string name, string surname, string phoneNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        public static User Registry(IDataBase database, string phoneNumber, string pass)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@pass", pass),
                new SqlParameter("@phone", phoneNumber)
            };
            database.InsertUpdateDeleteData(SqlQueries.RegistryUserQuery, parameters);
            DataTable result = TakeUser(database, phoneNumber, pass);
            return CreateUserFromDataTable(result, phoneNumber);
        }

        public static User Authentication(IDataBase database, string phoneNumber, string password)
        {
            DataTable result = TakeUser(database, phoneNumber, password);
            return CreateUserFromDataTable(result, phoneNumber);
        }

        private static DataTable TakeUser(IDataBase database, string phoneNumber, string pass)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@PhoneNumber", phoneNumber),
                new SqlParameter("@password", pass)
            };

            return database.SelectData(SqlQueries.CheckUserQuery, parameters);
        }

        private static User CreateUserFromDataTable(DataTable dataTable, string phoneNumber)
        {
            if (dataTable.Rows.Count == 1)
            {
                int id = Convert.ToInt32(dataTable.Rows[0]["id_user"]);
                string name = dataTable.Rows[0]["name"].ToString() ?? throw new Exception("Name cannot be null");
                string surname = dataTable.Rows[0]["surname"].ToString() ?? throw new Exception("Surname cannot be null");
                return new User(id, name, surname, phoneNumber);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public static Dictionary<bool, int> CheckHomeUser(IDataBase database, string city, string street, string home)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@city", city),
                new SqlParameter("@street", street),
                new SqlParameter("@home", home)
            };

            DataTable result = database.SelectData(SqlQueries.CheckHomeUserQuery, parameters);
            var res = new Dictionary<bool, int>();
            if (result.Rows.Count == 1)
            {
                res.Add(true, Convert.ToInt32(result.Rows[0]["id_residence"]));
            }
            else
            {
                res.Add(false, 0);
            }
            return res;
        }

        public static Dictionary<string, string> ShowCommunalForPayment(IDataBase database, int id)
        {
            var resultDictionary = new Dictionary<string, string>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", id)
            };

            DataTable result = database.SelectData(SqlQueries.ShowCommunalForPaymentQuery, parameters);
            if (result.Rows.Count == 1)
            {
                resultDictionary["gas"] = result.Rows[0]["gas"].ToString();
                resultDictionary["electricity"] = result.Rows[0]["electricity"].ToString();
                resultDictionary["internet"] = result.Rows[0]["internet"].ToString();
                return resultDictionary;
            }
            else
            {
                throw new Exception("Communal payment details not found");
            }
        }
    }
}
