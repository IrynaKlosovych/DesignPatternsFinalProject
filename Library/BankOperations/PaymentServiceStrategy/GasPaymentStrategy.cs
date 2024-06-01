using Library.Accounts;
using Library.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public class GasPaymentStrategy : IServicePaymentStrategy
    {
        private decimal _sum;
        private int _id;
        private string _card;
        private DataBase instance;
        public GasPaymentStrategy(decimal sum, int idResidence, string card, DataBase instance)
        {
            _sum = sum;
            _id = idResidence;
            _card = card;
            this.instance = instance;
        }
        public void Pay()
        {
            PayGas();
            BankOperations.UpdateMoney(instance, _card, -_sum);
            string description = $"Ви сплатили {_sum} за газ";
            Card.AddTransactionWithCardToHistory(instance,
                    DateTime.Now, description,
                    -_sum, _card);
            ForConsoleOperations.ShowResultOfOperation(description);
        }
        private void PayGas()
        {
            string query = "update Residence set gas = gas + @sum where id_residence = @id";
            SqlParameter[] parameters = new SqlParameter[]
           {
            new SqlParameter("@sum", _sum),
            new SqlParameter("@id", _id)
           };
            instance.InsertUpdateDeleteData(query, parameters);
        }
    }
}
