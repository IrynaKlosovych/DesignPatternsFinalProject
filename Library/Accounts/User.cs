using Library.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string checkQuery = "SELECT u.id_user, surname, name FROM MyUser u INNER JOIN Phone p ON u.id_tel = p.id_phone WHERE p.phone_number = @PhoneNumber AND u.pass = @password;";
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
                int id = Convert.ToInt32(dataTable.Rows[0]["id_user"]);
                string name = dataTable.Rows[0]["name"].ToString() ?? throw new Exception("Name cannot be null");
                string surname = dataTable.Rows[0]["surname"].ToString() ?? throw new Exception("Surname cannot be null");
                return new User(id, name, surname, phoneNumber);
            }
            else
            {
                throw new Exception();
            }
        }

        public static Dictionary<bool, int> CheckHomeUser(DataBase instance, string city, string street, string home)
        {
            string checkQuery = "select id_residence from Residence where city = @city and street = @street and home = @home";
            SqlParameter[] newParameters = new SqlParameter[]
            {
            new SqlParameter("@city", city),
            new SqlParameter("@street", street),
            new SqlParameter("@home", home)
            };

            DataTable result = instance.SelectData(checkQuery, newParameters);
            Dictionary<bool, int> res = new Dictionary<bool, int>();
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

        public static Dictionary<string, string> ShowCommunalForPayment(DataBase instance, int id)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            string checkQuery = "select gas, electricity, internet from Residence where id_residence = @id";
            SqlParameter[] newParameters = new SqlParameter[]
            {
            new SqlParameter("@id", id),
            };

            DataTable result = instance.SelectData(checkQuery, newParameters);
            if (result.Rows.Count == 1)
            {
                resultDictionary["gas"] = result.Rows[0]["gas"].ToString()!;
                resultDictionary["electricity"] = result.Rows[0]["electricity"].ToString()!;
                resultDictionary["internet"] = result.Rows[0]["internet"].ToString()!;
                return resultDictionary;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
