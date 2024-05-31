using Library.Accounts;
using Library.BankOperations;
using Library.BankOperations.Handlers;
using Library.DB;
using System.Net.NetworkInformation;

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";
System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

DataBase database = DataBase.GetInstance();

Console.WriteLine("Вітаємо на головній сторінці!");
Console.WriteLine("Увійдіть або зареєструйтесь, щоби мати можливість здійснювати прості банківські операції");
bool isValidInput = false;
User user = null!;
do
{
    Console.WriteLine("Введіть номер телефону:");
    string? phoneNumber = Console.ReadLine();
    if (phoneNumber == null || !Phone.IsValidMobilePhoneNumber(phoneNumber))
    {
        Console.WriteLine("Ви не ввели номер телефону або номер не валідний");
    }
    else
    {
        if (Phone.IsExistPhoneInDB(database, phoneNumber))
        {
            Console.WriteLine("Акаунт уже існує");
            string pass = CheckPass();
            try
            {
                user = User.Authentication(database, phoneNumber, pass);
                isValidInput = true;
            }
            catch
            {
                Console.WriteLine($"Деяка помилка, введіть дані знову");
            }
        }
        else
        {
            Console.WriteLine("Акаунт не існує, створімо його");
            string? pass2;
            string mainPass = CheckPass();
            do
            {
                Console.WriteLine("Введіть ще раз пароль:");
                pass2 = Console.ReadLine();
                if (!Password.ComparePasswords(mainPass, pass2))
                {
                    Console.WriteLine("Паролі не збігаються");
                }
            } while (!Password.ComparePasswords(mainPass, pass2));
            try
            {
                user = User.Registry(database, phoneNumber, mainPass);
                isValidInput = true;
            }
            catch
            {
                Console.WriteLine($"Деяка помилка, введіть дані знову");
            }

        }
    }
} while (!isValidInput);

static string CheckPass()
{
    string? pass;
    do
    {
        Console.WriteLine("Введіть пароль (більше 7 знаків, повинен містити цифри та літери):");
        pass = Console.ReadLine();

        if (!Password.IsValidPass(pass))
        {
            Console.WriteLine("Пароль не валідний");
        }
    } while (!Password.IsValidPass(pass));
    return pass!;
}

Console.Clear();

string? choosenOperation;
ForConsoleOperations console = new ForConsoleOperations(user, database);
IHandler interceptors = ClientOperations.GetInterceptors(console);

do
{
    Console.WriteLine($"Вітаємо, {user.Surname} {user.Name}");
    Console.WriteLine("0. Завершити\n" +
        "1. Переглянути баланс на всіх картках\n" +
        "2. Перекази з одної картки на іншу\n" +
        "3. Поповнити телефон\n" +
        "4. Комунальні платежі\n" +
        "5. Змінити пінкод картки\n" +
        "6. Історія\n");
    Console.WriteLine("Виберіть операцію(введіть цифру):");

    choosenOperation = Console.ReadLine();

    if (choosenOperation != null)
        interceptors.Handle(choosenOperation);
    else Console.WriteLine("Ви не обрали жодну операцію");

    Console.ReadKey();
    Console.Clear();
} while (choosenOperation!="0");
