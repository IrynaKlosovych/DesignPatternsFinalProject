using Library.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class ChangePinCode:BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public ChangePinCode(ForConsoleOperations console)
        {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "5")
            {
                string ownCard = consoleOperations.ChooseOwnCard();

                string pincode = consoleOperations.ChoosePinCode();

                Card.ChangePin(consoleOperations.Instance, pincode, ownCard);

                string description = $"Ви змінили пінкод на картці {ownCard}";
                consoleOperations.ShowResultOfOperation(description);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
