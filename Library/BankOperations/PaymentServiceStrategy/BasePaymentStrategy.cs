using Library.DB;
using Library.Accounts;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public abstract class BasePaymentStrategy : IServicePaymentStrategy
    {
        protected decimal _sum;
        protected int _id;
        protected string _card;
        protected IDataBase instance;

        public BasePaymentStrategy(decimal sum, int idResidence, string card, IDataBase instance)
        {
            _sum = sum;
            _id = idResidence;
            _card = card;
            this.instance = instance;
        }

        public void Pay()
        {
            ExecutePayment();
            BankOperations.UpdateMoney((DataBase)instance, _card, -_sum);
            string description = GetDescription();
            var card = new Card(instance);
            card.AddTransactionWithCardToHistory(DateTime.Now, description, -_sum, _card);
            ForConsoleOperations.ShowResultOfOperation(description);
        }

        protected abstract void ExecutePayment();
        protected abstract string GetDescription();
    }
}
