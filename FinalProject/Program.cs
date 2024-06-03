using Library.Accounts;
using Library.BankOperations;
using Library.BankOperations.Handlers;
using Library.BankOperations.PaymentServiceStrategy;
using Library.DB;
using Library.Template;
using System.Net.NetworkInformation;

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";
System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

DataBase database = DataBase.GetInstance();

Console.WriteLine("Вітаємо на головній сторінці!");
Console.WriteLine("Увійдіть або зареєструйтесь, щоби мати можливість здійснювати прості банківські операції");

User user = AuthenticateOrRegisterUser(database);
Console.Clear();

ForConsoleOperations console = new ForConsoleOperations(user, database);
IHandler interceptors = ClientOperations.GetInterceptors(console);

MainMenu(user, interceptors);

static User AuthenticateOrRegisterUser(DataBase database)
{
    while (true)
    {
        Console.WriteLine("Введіть номер телефону:");
        string? phoneNumber = Console.ReadLine();
        if (phoneNumber == null || !Phone.IsValidMobilePhoneNumber(phoneNumber))
        {
            Console.WriteLine("Ви не ввели номер телефону або номер не валідний");
            continue;
        }

        var phoneService = new Phone(database);
        UserOperation userOperation = phoneService.IsExistPhoneInDB(phoneNumber)
            ? new AuthenticateUserOperation(database)
            : new RegisterUserOperation(database);

        User user = userOperation.Execute(phoneNumber);
        if (user != null)
        {
            return user;
        }
       
    }
}



static void MainMenu(User user, IHandler interceptors)
{
    string? choosenOperation;

    do
    {
        DisplayMenu(user);
        choosenOperation = Console.ReadLine();

        if (choosenOperation != null)
        {
            interceptors.Handle(choosenOperation);
        }
        else
        {
            Console.WriteLine("Ви не обрали жодну операцію");
        }

        Console.WriteLine("Введіть Enter");
        Console.ReadKey();
        Console.Clear();
    } while (choosenOperation != "0");
}

static void DisplayMenu(User user)
{
    Console.WriteLine($"Вітаємо, {user.Surname} {user.Name}");
    Console.WriteLine("0. Завершити\n" +
                      "1. Переглянути баланс на всіх картках\n" +
                      "2. Перекази з одної картки на іншу\n" +
                      "3. Поповнити телефон\n" +
                      "4. Комунальні платежі(оплата)\n" +
                      "5. Змінити пінкод картки\n" +
                      "6. Історія\n");
    Console.WriteLine("Виберіть операцію(введіть цифру):");
}
