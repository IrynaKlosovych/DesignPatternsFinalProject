using Library.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class AddMoneyToAnotherCard : BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public AddMoneyToAnotherCard(ForConsoleOperations console)
        {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "2")
            {
                string ownCard = consoleOperations.ChooseOwnCard();

                string anotherCard = consoleOperations.ChooseAnotherCard();

                decimal cardBalance = Card.TakeBalanceFromCard(consoleOperations.Instance, ownCard, consoleOperations.UserInfo.Id);
                decimal sumForTransaction;
                do
                {
                    sumForTransaction = consoleOperations.ChooseSumForTransaction();
                } while (sumForTransaction > cardBalance);

                BankOperations.UpdateMoney(consoleOperations.Instance, ownCard, -sumForTransaction);

                BankOperations.UpdateMoney(consoleOperations.Instance, anotherCard, sumForTransaction);

                string owndescription = $"Ви переказали {sumForTransaction} грн на картку {anotherCard}";
                string anotherdescription = $"Ви отримали {sumForTransaction} грн на картку {anotherCard}";

                Card.AddTransactionWithCardToHistory(consoleOperations.Instance,
                    DateTime.Now, owndescription,
                    -sumForTransaction, ownCard);

                Card.AddTransactionWithCardToHistory(consoleOperations.Instance,
                    DateTime.Now, anotherdescription,
                    sumForTransaction, anotherCard);

                ForConsoleOperations.ShowResultOfOperation(owndescription);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
