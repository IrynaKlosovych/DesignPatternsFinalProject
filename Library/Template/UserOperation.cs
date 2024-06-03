using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Template
{
    public abstract class UserOperation
    {
        protected DataBase Database { get; }
        protected Phone PhoneService { get; }

        protected UserOperation(DataBase database)
        {
            Database = database;
            PhoneService = new Phone(database);
        }

        public User Execute(string phoneNumber)
        {
            return HandleUserOperation(phoneNumber);
        }

        protected abstract User HandleUserOperation(string phoneNumber);


        protected string CheckPass()
        {
            while (true)
            {
                string? pass = Input("Введіть пароль (більше 7 знаків, повинен містити цифри та літери):");

                if (Password.IsValidPass(pass))
                {
                    return pass!;
                }
                Console.WriteLine("Пароль не валідний");
            }
        }

        protected string? Input(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
