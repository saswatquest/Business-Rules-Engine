using System;
using Business_Rules_Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business_Rules_Engine.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void DisplayRulesTest()
        {
            PaymentPage page = new PaymentPage();
           
            page.InitiatePayment();
            
        }
    }
}


