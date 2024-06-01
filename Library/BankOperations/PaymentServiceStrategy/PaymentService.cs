using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.PaymentServiceStrategy
{
    public class PaymentService
    {
        private IServicePaymentStrategy _paymentStrategy;
        public PaymentService(IServicePaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }
        public void MakePayment()
        {
            if (_paymentStrategy != null)
            {
                _paymentStrategy.Pay();
            }
        }
    }
}
