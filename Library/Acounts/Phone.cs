using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Acounts
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

        }
    }
}
