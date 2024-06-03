using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class AddMoneyToPhone : BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public AddMoneyToPhone(ForConsoleOperations console)
        {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "3")
            {
                string ownCard = consoleOperations.ChooseOwnCard();
                string phoneNumber = consoleOperations.ChoosePhoneNumber();

                var cardService = new Card(consoleOperations.Instance);
                decimal cardBalance = cardService.TakeBalanceFromCard(ownCard, consoleOperations.UserInfo.Id);
                decimal sumForTransaction;
                do
                {
                    sumForTransaction = consoleOperations.ChooseSumForTransaction();
                } while (sumForTransaction > cardBalance);

                BankOperations.UpdateMoney((DataBase)consoleOperations.Instance, ownCard, -sumForTransaction);

                var phoneService = new Phone(consoleOperations.Instance);
                phoneService.AddMoneyToPhone(phoneNumber, sumForTransaction);

                string description = $"Ви поповнили номер {phoneNumber} на {sumForTransaction} грн";
                cardService.AddTransactionWithCardToHistory(DateTime.Now, description, -sumForTransaction, ownCard);

                ForConsoleOperations.ShowResultOfOperation(description);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
