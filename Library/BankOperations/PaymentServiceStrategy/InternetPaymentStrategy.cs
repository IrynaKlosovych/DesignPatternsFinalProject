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
    public class InternetPaymentStrategy: IServicePaymentStrategy
    {
        private decimal _sum;
        private int _id;
        private string _card;
        private DataBase instance;
        public InternetPaymentStrategy(decimal sum, int idResidence, string card, DataBase instance)
        {
            _sum = sum;
            _id = idResidence;
            _card = card;
            this.instance = instance;
        }
        public void Pay()
        {
            PayInternet();
            BankOperations.UpdateMoney(instance, _card, -_sum);
            string description = $"Ви сплатили {_sum} за інтернет";
            Card.AddTransactionWithCardToHistory(instance,
                    DateTime.Now, description,
                    -_sum, _card);
            ForConsoleOperations.ShowResultOfOperation(description);
        }
        private void PayInternet()
        {
            string query = "update Residence set internet = internet + @sum where id_residence = @id";
            SqlParameter[] parameters = new SqlParameter[]
           {
            new SqlParameter("@sum", _sum),
            new SqlParameter("@id", _id)
           };
            instance.InsertUpdateDeleteData(query, parameters);
        }
    }
}
