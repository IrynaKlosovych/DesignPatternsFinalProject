using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Template
{
    public class AuthenticateUserOperation : UserOperation
    {
        public AuthenticateUserOperation(DataBase database) : base(database) { }

        protected override User HandleUserOperation(string phoneNumber)
        {
            Console.WriteLine("Акаунт уже існує");
            string pass = CheckPass();

            try
            {
                return User.Authentication(Database, phoneNumber, pass);
            }
            catch
            {
                Console.WriteLine("Деяка помилка, введіть дані знову");
                return null;
            }
        }
    }
}
