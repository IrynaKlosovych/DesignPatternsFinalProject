using Library.DB;
using System.Net.NetworkInformation;

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";
System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

DataBase database = DataBase.GetInstance();
Console.WriteLine("Вітаємо на головній сторінці!");
Console.WriteLine("Виберіть пункт для входу, щоби мати можливість здійснювати прості банківські операції:");
Console.WriteLine("1 - реєстрація в застосунку, 2 - автентифікація");
string? choosingNumber = Console.ReadLine();
bool isValidInput = false;
do
{
    if(choosingNumber==null)
    {
        Console.WriteLine("Ви не ввели відповідь");
    }
    else
    {
        switch (choosingNumber)
        {
            case "1":
                Console.WriteLine("Введіть номер телефону:");
                //Check phone number

                Console.WriteLine("Введіть пароль:");

                Console.WriteLine("Введіть ще раз пароль:");
                //compare passwords

                isValidInput = true;
                break;
            case "2":
                Console.WriteLine("Введіть номер телефону:");
                //Check phone number

                Console.WriteLine("Введіть пароль:");
                //check have user this number + pass

                isValidInput = true;
                break;
            default:
                Console.WriteLine("Невірний вибір. Будь ласка, виберіть 1 або 2.");
                break;
        }
    }
} while (!isValidInput);
Console.Clear();
Console.WriteLine("Виберіть операцію:");
Console.WriteLine("1. Переглянути баланс на всіх картках\n" +
    "2. Перекази з одної картки на іншу\n" +
    "3. Поповнити телефон\n" +
    "4. Комунальні платежі\n" +
    "5. Змінити пінкод картки\n" +
    "6. Історія\n");
string? choosenOperation = Console.ReadLine();