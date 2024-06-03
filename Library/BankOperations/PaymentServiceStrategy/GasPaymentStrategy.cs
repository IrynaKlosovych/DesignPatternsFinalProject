using Library.DB;
using System.Data.SqlClient;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public class GasPaymentStrategy : BasePaymentStrategy
    {
        public GasPaymentStrategy(decimal sum, int idResidence, string card, IDataBase instance)
            :base(sum, idResidence, card, instance)
        {

        }

        protected override void ExecutePayment()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@sum", _sum),
                new SqlParameter("@id", _id)
            };
            instance.InsertUpdateDeleteData(SqlQueries.PayGasQuery, parameters);
        }

        protected override string GetDescription()
        {
            return $"Ви сплатили {_sum} за газ";
        }
    }
}
