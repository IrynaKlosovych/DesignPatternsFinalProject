using Library.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Accounts
{
    public static class Phone
    {
        public static bool IsValidMobilePhoneNumber(string phoneNumber)
        {
            string pattern = @"^0(39|50|6[3|6-8]|73|89|9[1-9])\d{7}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static bool IsExistPhoneInDB(DataBase instance, string phoneNumber)
        {
            string query = "SELECT u.id_user FROM MyUser u INNER JOIN Phone p ON u.id_tel = p.id_phone WHERE p.phone_number = @PhoneNumber AND u.pass is Not null;";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@PhoneNumber", phoneNumber)
            };

            DataTable result = instance.SelectData(query, parameters);
            if (result.Rows.Count == 1)
            {
                return true;
            }
            return false;
        }
    }
}
