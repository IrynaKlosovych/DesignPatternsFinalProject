﻿using Library.BankOperations.Handlers.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers
{
    public static class ClientOperations
    {
        public static IHandler GetInterceptors(ForConsoleOperations console)
        {
            var first = new CardsBalanceHandler(console);

            first.SetNext(new AddMoneyToAnotherCard(console))
                .SetNext(new AddMoneyToPhone(console))
                .SetNext(new ChangePinCode(console))
                .SetNext(new ShowHistory(console));
            return first;
        }
    }
}