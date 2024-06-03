using Library.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class CardsBalanceHandler: BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public CardsBalanceHandler(ForConsoleOperations console) {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "1")
            {
                var result = BankOperations.CheckBalanceOnAllCards((DataBase)consoleOperations.Instance, consoleOperations.UserInfo.Id);
                consoleOperations.ShowCardBalance(result);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
