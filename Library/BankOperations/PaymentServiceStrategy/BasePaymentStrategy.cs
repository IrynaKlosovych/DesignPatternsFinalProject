using Library.DB;
using Library.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public abstract class BasePaymentStrategy : IServicePaymentStrategy
    {
        protected decimal _sum;
        protected int _id;
        protected string _card;
        protected DataBase instance;

        public BasePaymentStrategy(decimal sum, int idResidence, string card, DataBase instance)
        {
            _sum = sum;
            _id = idResidence;
            _card = card;
            this.instance = instance;
        }

        public void Pay()
        {
            ExecutePayment();
            BankOperations.UpdateMoney(instance, _card, -_sum);
            string description = GetDescription();
            Card.AddTransactionWithCardToHistory(instance, DateTime.Now, description, -_sum, _card);
            ForConsoleOperations.ShowResultOfOperation(description);
        }

        protected abstract void ExecutePayment();
        protected abstract string GetDescription();
    }
}
