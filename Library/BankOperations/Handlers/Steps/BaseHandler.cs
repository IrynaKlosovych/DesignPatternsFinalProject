using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BankOperations.Handlers.Steps
{
    public class BaseHandler : IHandler
    {
        private IHandler? next;
        public IHandler SetNext(IHandler next)
        {
            this.next = next;
            return next;
        }

        public virtual void Handle(string request)
        {
            if (this.next != null)
            {
                this.next.Handle(request);
            }
            else
            {
                return;
            }
        }
    }
}
