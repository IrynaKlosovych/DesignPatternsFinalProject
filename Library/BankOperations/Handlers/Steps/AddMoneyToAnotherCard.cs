using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class AddMoneyToAnotherCard: BaseHandler
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
                decimal sumForTransaction = consoleOperations.ChooseSumForTransaction();
                //3. check own card balance
                //4. withdraw from own
                //5. add to another
                //6. add to history
                //7. show result
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
