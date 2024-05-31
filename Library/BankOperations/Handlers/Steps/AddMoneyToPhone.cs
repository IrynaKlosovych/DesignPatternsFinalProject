using Library.Accounts;
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

                decimal cardBalance = Card.TakeBalanceFromCard(consoleOperations.Instance, ownCard, consoleOperations.UserInfo.Id);
                decimal sumForTransaction;
                do
                {
                    sumForTransaction = consoleOperations.ChooseSumForTransaction();
                } while (sumForTransaction > cardBalance);

                BankOperations.UpdateMoney(consoleOperations.Instance, ownCard, -sumForTransaction);
                Phone.AddMoneyToPhone(consoleOperations.Instance, phoneNumber, sumForTransaction);

                string owndescription = $"Ви переказали {sumForTransaction} грн на номер телефону {phoneNumber}";

                Card.AddTransactionWithCardToHistory(consoleOperations.Instance,
                    DateTime.Now, owndescription,
                    -sumForTransaction, ownCard);

                consoleOperations.ShowResultOfOperation(owndescription);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
