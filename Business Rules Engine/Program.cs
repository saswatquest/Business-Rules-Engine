using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Rules_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentPage PayPage = new PaymentPage();
            PayPage.InitiatePayment();
        }
    }
}
