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

        public string ChooseOwnCard()
        {
            string resultCard="";
            string? card;
            bool ok = false;
            do
            {
                Console.WriteLine("Оберіть картку, з якої знімаєте кошти:");
                card = Console.ReadLine();
                if (card != null)
                {
                    ok = Card.CheckOwnCard(Instance, card, UserInfo.Id);
                    if (ok) resultCard = card;
                }
            } while (!ok);
            return resultCard;
        }

        public string ChooseAnotherCard()
        {
            string resultCard = "";
            string? card;
            bool ok = false;
            do
            {
                Console.WriteLine("Оберіть картку, куди перекидаєте кошти:");
                card = Console.ReadLine();
                if (card != null)
                {
                    ok = Card.IsExistAnotherCard(Instance, card);
                    if (ok) resultCard = card;
                }
            } while (!ok);
            return resultCard;
        }
        public decimal ChooseSumForTransaction()
        {
            bool success = false;
            decimal resultSum = 0;
            do
            {
                Console.WriteLine("Введіть суму для виконання операції:");
                string? input = Console.ReadLine();
                success = decimal.TryParse(input, out decimal sum);
                if (success&&sum>0)
                {
                    resultSum = sum;
                }
            } while (!success);
            return resultSum;
        }
    }
}
