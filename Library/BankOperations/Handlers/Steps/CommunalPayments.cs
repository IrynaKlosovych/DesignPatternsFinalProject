using Library.Accounts;
using Library.BankOperations.PaymentServiceStrategy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class CommunalPayments: BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public CommunalPayments(ForConsoleOperations console)
        {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "4")
            {
                int residence = consoleOperations.ChooseHomeForCommunalPayment();
                consoleOperations.ShowCommunalInfo(residence);
                string ownCard = consoleOperations.ChooseOwnCard();

                var cardService = new Card(consoleOperations.Instance);
                decimal cardBalance = cardService.TakeBalanceFromCard(ownCard, consoleOperations.UserInfo.Id);

                List<IServicePaymentStrategy> strategies = consoleOperations.CheckSumForPayment(cardBalance, residence, ownCard);
                PaymentService paymentService = new PaymentService(new MultipleServicesPaymentStrategy(strategies));
                paymentService.MakePayment();

                ForConsoleOperations.ShowResultOfOperation("Ви сплатили за комунальні послуги");
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
