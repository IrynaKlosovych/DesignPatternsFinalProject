using Library.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Acounts
{
    public class User
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string PhoneNumber { get; private set; }

        private User(string name, string surname, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        public static User Registry(DataBase instance, string phoneNumber, string pass)
        {
            string query = "UPDATE MyUser SET pass = @pass WHERE id_tel = (SELECT id_phone FROM Phone WHERE phone_number = @phone)";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@pass", pass),
            new SqlParameter("@phone", phoneNumber)
            };
            instance.InsertUpdateDeleteData(query, parameters);
            DataTable result = TakeUser(instance, phoneNumber, pass);
            return CreateUserFromDataTable(result, phoneNumber);
        }

        public static User Authentication(DataBase instance, string phoneNumber, string password)
        {
            DataTable result = TakeUser(instance, phoneNumber, password);
            return CreateUserFromDataTable(result, phoneNumber);
        }

        private static DataTable TakeUser(DataBase instance, string phoneNumber, string pass)
        {
            string checkQuery = "SELECT surname, name FROM MyUser u INNER JOIN Phone p ON u.id_tel = p.id_phone WHERE p.phone_number = @PhoneNumber AND u.pass = @password;";
            SqlParameter[] newParameters = new SqlParameter[]
            {
            new SqlParameter("@PhoneNumber", phoneNumber),
            new SqlParameter("@password", pass)
            };

            DataTable result = instance.SelectData(checkQuery, newParameters);
            return result;
        }

        private static User CreateUserFromDataTable(DataTable dataTable, string phoneNumber)
        {
            if (dataTable.Rows.Count == 1)
            {
                string name = dataTable.Rows[0]["name"].ToString() ?? throw new Exception("Name cannot be null");
                string surname = dataTable.Rows[0]["surname"].ToString() ?? throw new Exception("Surname cannot be null");
                return new User(name, surname, phoneNumber);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
