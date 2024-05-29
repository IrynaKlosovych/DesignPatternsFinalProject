using System.Net.NetworkInformation;

System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
customCulture.NumberFormat.NumberDecimalSeparator = ".";
System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;


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

                isValidInput = true;
                break;
            case "2":

                isValidInput = true;
                break;
            default:
                Console.WriteLine("Невірний вибір. Будь ласка, виберіть 1 або 2.");
                break;
        }
    }
} while (!isValidInput);