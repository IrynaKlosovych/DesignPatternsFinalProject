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
    public class ElectricityPaymentStrategy : BasePaymentStrategy
    {
        public ElectricityPaymentStrategy(decimal sum, int idResidence, string card, DataBase instance)
            : base(sum, idResidence, card, instance)
        {
        }

        protected override void ExecutePayment()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@sum", _sum),
                new SqlParameter("@id", _id)
            };
            instance.InsertUpdateDeleteData(SqlQueries.PayElectricityQuery, parameters);
        }

        protected override string GetDescription()
        {
            return $"Ви сплатили {_sum} за електроенергію";
        }
    }
}
