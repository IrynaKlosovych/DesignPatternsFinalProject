using Library.Accounts;
using Library.DB;
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

                var cardService = new Card(consoleOperations.Instance);
                decimal cardBalance = cardService.TakeBalanceFromCard(ownCard, consoleOperations.UserInfo.Id);
                decimal sumForTransaction;
                do
                {
                    sumForTransaction = consoleOperations.ChooseSumForTransaction();
                } while (sumForTransaction > cardBalance);

                BankOperations.UpdateMoney((DataBase)consoleOperations.Instance, ownCard, -sumForTransaction);
                BankOperations.UpdateMoney((DataBase)consoleOperations.Instance, anotherCard, sumForTransaction);

                string owndescription = $"Ви переказали {sumForTransaction} грн на картку {anotherCard}";
                string anotherdescription = $"Ви отримали {sumForTransaction} грн на картку {anotherCard}";

                cardService.AddTransactionWithCardToHistory(DateTime.Now, owndescription, -sumForTransaction, ownCard);
                cardService.AddTransactionWithCardToHistory(DateTime.Now, anotherdescription, sumForTransaction, anotherCard);

                ForConsoleOperations.ShowResultOfOperation(owndescription);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
