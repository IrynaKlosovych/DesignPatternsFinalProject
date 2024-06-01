using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class ShowHistory: BaseHandler
    {
        private ForConsoleOperations consoleOperations;
        public ShowHistory(ForConsoleOperations console)
        {
            consoleOperations = console;
        }

        public override void Handle(string request)
        {
            if (request == "6")
            {
                DataTable dataTable = BankOperations.ShowHistory(consoleOperations.Instance, consoleOperations.UserInfo.Id);
                consoleOperations.ShowHistory(dataTable);
            }
            else
            {
                base.Handle(request);
            }
        }
    }
}
