using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Template
{
    public class RegisterUserOperation : UserOperation
    {
        public RegisterUserOperation(DataBase database) : base(database) { }

        protected override User HandleUserOperation(string phoneNumber)
        {
            Console.WriteLine("Акаунт не існує, створімо його");
            string mainPass = CheckPass();

            while (true)
            {
                string? pass2 = Input("Введіть ще раз пароль:");
                if (Password.ComparePasswords(mainPass, pass2))
                {
                    try
                    {
                        return User.Registry(Database, phoneNumber, mainPass);
                    }
                    catch
                    {
                        Console.WriteLine("Деяка помилка");
                        return null!;
                    }
                }
                Console.WriteLine("Паролі не збігаються");
            }
        }
    }
}
