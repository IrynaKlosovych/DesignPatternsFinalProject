using Library.Accounts;
using Library.BankOperations.PaymentServiceStrategy;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Data;
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
                if (success&&sum>=0)
                {
                    resultSum = sum;
                }
            } while (!success);
            return resultSum;
        }

        public static void ShowResultOfOperation(string description)
        {
            Console.WriteLine(description);
        }

        public string ChoosePhoneNumber()
        {
            string resultPhone= "";
            string? phone;
            bool ok = false;
            do
            {
                Console.WriteLine("Оберіть номер телефону, який поповнюєте:");
                phone = Console.ReadLine();
                if (phone != null)
                {
                    ok = Phone.CheckPhone(Instance, phone);
                    if (ok) resultPhone = phone;
                }
            } while (!ok);
            return resultPhone;
        } 
        public string ChoosePinCode()
        {
            string resultPin = "";
            string? pin;
            bool ok = false;
            do
            {
                Console.WriteLine("Введіть новий пінкод:");
                pin = Console.ReadLine();
                if (pin != null)
                {
                    ok = Card.CheckPinCodeWriting(pin);
                    if (ok) resultPin = pin;
                }
            } while (!ok);
            return resultPin;
        }

        public void ShowHistory(DataTable history)
        {
            int dateWidth = 20;
            int descriptionWidth = 100;
            int sumaWidth = 10;
            int cardNumberWidth = 20;

            Console.WriteLine($"{"Дата".PadRight(dateWidth)} | {"Опис".PadRight(descriptionWidth)} | {"Сума".PadRight(sumaWidth)} | {"Картка".PadRight(cardNumberWidth)}");
            Console.WriteLine(new string('-', dateWidth + descriptionWidth + sumaWidth + cardNumberWidth));

            foreach (DataRow row in history.Rows)
            {
                Console.WriteLine($"{row["date"].ToString()!.PadRight(dateWidth)} | {row["description"].ToString()!.PadRight(descriptionWidth)} | {row["suma"].ToString()!.PadRight(sumaWidth)} | {row["card_number"].ToString()!.PadRight(cardNumberWidth)}");
            }
        }

        public int ChooseHomeForCommunalPayment()
        {
            int result=0;
            string? city;
            string? street;
            string? home;
            bool ok = false;
            do
            {
                Console.WriteLine("Приклад введення даних: Київ Шевчека 10А");
                Console.WriteLine("Введіть місто:");
                city = Console.ReadLine();   
                Console.WriteLine("Введіть вулицю:");
                street = Console.ReadLine();               
                Console.WriteLine("Введіть будинок(квартиру):");
                home = Console.ReadLine();
                if (city != null && street !=null && home!=null)
                {
                    var some = User.CheckHomeUser(Instance, city, street, home);
                    if (some.ContainsKey(true))
                    {
                        result = some[true];
                        ok=true;
                    }
                }
            } while (!ok);
            return result;
        }

        public void ShowCommunalInfo(int residence)
        {
            var result = User.ShowCommunalForPayment(Instance, residence);
            Console.WriteLine($"Газ: {result["gas"]}\nЕлектрика: {result["electricity"]}\nІнтернет: {result["internet"]}");
        }

        public List<IServicePaymentStrategy> CheckSumForPayment(decimal ownBalance, int residenceID, string card)
        {
            List<IServicePaymentStrategy> result = new List<IServicePaymentStrategy>();
            bool success1, success2, success3 = false;
            decimal gas = 0, electricity = 0, internet = 0, sum=0;
            do
            {
                Console.WriteLine("Введіть оплату за газ:");
                string? input = Console.ReadLine();
                success1 = decimal.TryParse(input, out gas); 
                Console.WriteLine("Введіть оплату за електрику:");
                input = Console.ReadLine();
                success2 = decimal.TryParse(input, out electricity);
                Console.WriteLine("Введіть оплату за інтернет:");
                input = Console.ReadLine();
                success3 = decimal.TryParse(input, out internet);
                sum=gas+electricity+internet;
                if (sum > ownBalance)
                {
                    Console.WriteLine($"Загальна сума {sum} перевищує ваш баланс {ownBalance}. Будь ласка, спробуйте ще раз.");
                }
                else
                {
                    if (success1) result.Add(new GasPaymentStrategy(gas, residenceID, card, Instance));
                    if (success2) result.Add(new ElectricityPaymentStrategy(electricity, residenceID, card, Instance));
                    if (success3) result.Add(new InternetPaymentStrategy(internet, residenceID, card, Instance));
                }
            } while (sum>ownBalance);
            return result;
        }
    }
}
