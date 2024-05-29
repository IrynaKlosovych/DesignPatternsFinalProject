using Library.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Acounts
{
    public class User
    {
        public string Name {  get; private set; }
        public string Surname { get; private set; }
        public string PhoneNumber {  get; private set; }

        public User(string name, string surname, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
        }

        public static User Registry(DataBase instance)
        {

        }

        public static User Authentication(DataBase instance)
        {

        }
    }
}
