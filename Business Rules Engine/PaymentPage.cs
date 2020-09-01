using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_Rules_Engine
{
    public class PaymentPage : PaymentEngine
    {
        public void InitiatePayment()
        {
            DisplayRules();
        }
    }
}
