using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class SqlQueries
    {
        // Phone.cs
        public const string IsExistPhoneInDBQuery = "SELECT u.id_user FROM MyUser u INNER JOIN Phone p ON u.id_tel = p.id_phone WHERE p.phone_number = @PhoneNumber AND u.pass is Not null;";
        public const string CheckPhoneQuery = "SELECT id_phone FROM Phone WHERE phone_number = @PhoneNumber;";
        public const string AddMoneyToPhoneQuery = "update Phone set suma=suma+@sum where phone_number = @phone";

        // User.cs
        public const string RegistryUserQuery = "UPDATE MyUser SET pass = @pass WHERE id_tel = (SELECT id_phone FROM Phone WHERE phone_number = @phone)";
        public const string CheckUserQuery = "SELECT u.id_user, surname, name FROM MyUser u INNER JOIN Phone p ON u.id_tel = p.id_phone WHERE p.phone_number = @PhoneNumber AND u.pass = @password;";
        public const string CheckHomeUserQuery = "select id_residence from Residence where city = @city and street = @street and home = @home";
        public const string ShowCommunalForPaymentQuery = "select gas, electricity, internet from Residence where id_residence = @id";

        // Card.cs
        public const string IsExistAnotherCardQuery = "select id_card from CARD where card_number = @card";
        public const string CheckOwnCardQuery = "select id_card from CARD c inner join MyUser u on c.id_user = u.id_user where card_number = @card and u.id_user=@id";
        public const string TakeBalanceFromCardQuery = "select balance from CARD c inner join MyUser u on c.id_user=u.id_user where u.id_user = @id and card_number = @card";
        public const string AddTransactionWithCardToHistoryQuery = "insert into History(date, description, suma, id_user, id_card) values(@Date, @Description, @sum, (select u.id_user from MyUser u inner join CARD c on u.id_user=c.id_user where card_number= @card), (select id_card from CARD  where card_number= @card))";
        public const string ChangePinQuery = "update CARD set pincode = @pin where card_number=@card";

        // BankOperations.cs
        public const string UpdateMoneyQuery = "UPDATE CARD SET balance = balance+@newBalance WHERE card_number= @card";
        public const string CheckBalanceOnAllCardsQuery = "select card_number, balance from CARD c inner join MyUser u on c.id_user=u.id_user where u.id_user = @id";
        public const string ShowHistoryQuery = "select date, description, suma, card_number from History inner join CARD on History.id_card = CARD.id_card where History.id_user = @id";

        // GasPaymentStrategy.cs
        public const string PayGasQuery = "update Residence set gas = gas + @sum where id_residence = @id";

        // InternetPaymentStrategy.cs
        public const string PayInternetQuery = "update Residence set internet = internet + @sum where id_residence = @id";

        // ElectricityPaymentStrategy.cs
        public const string PayElectricityQuery = "update Residence set electricity = electricity + @sum where id_residence = @id";
    }
}
