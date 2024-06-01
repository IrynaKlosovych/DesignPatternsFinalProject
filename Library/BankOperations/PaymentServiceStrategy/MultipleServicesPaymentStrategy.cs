using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public class MultipleServicesPaymentStrategy : IServicePaymentStrategy
    {
        private readonly List<IServicePaymentStrategy> _paymentStrategies;

        public MultipleServicesPaymentStrategy(List<IServicePaymentStrategy> paymentStrategies)
        {
            _paymentStrategies = paymentStrategies;
        }

        public void Pay()
        {
            foreach (var strategy in _paymentStrategies)
            {
                strategy.Pay();
            }
        }
    }
}
