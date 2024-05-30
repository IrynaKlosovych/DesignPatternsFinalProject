using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations
{
    public class ForConsoleOperations
    {
        public User UserInfo { get; private set; }
        public DataBase Instance { get; private set; }
        public ForConsoleOperations(User user, DataBase instance) {
            UserInfo = user;
            Instance = instance;
        }
        public void ShowCardBalance(Dictionary<string, decimal> cardBalance) {
            foreach(var card in cardBalance)
            {
                Console.WriteLine($"На картці {card.Key} {card.Value} грн");
            }
        }
    }
}
