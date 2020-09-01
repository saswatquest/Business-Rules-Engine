using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Rules_Engine
{
    public class PaymentEngine
    {
        private ExpandoObject RuleList;
        private Dictionary<string, string> RuleLibrary;
        public void DisplayRules()
        {
            
            RuleList = RulesEngine.Loadconfig(".\\BusinessRules.xml");

            int itemindex = 1;

            foreach(var rules in RuleList)
            {
                Console.WriteLine(itemindex.ToString() + ". " + rules.Key);
                itemindex++;
            }
            Console.WriteLine("New policies can be added in BusinessRules.xml to reflect in payemnt page");
            Console.WriteLine("Choose the index of policy...");
            string index = Console.ReadLine();

            if(int.Parse(index) > RuleList.Count())
            {
                Console.WriteLine("Invalid policy!!!");
                Console.WriteLine("Re Run the application...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Enter owner email:");
                string mail = Console.ReadLine();

                Console.WriteLine("Enter card details for payment:");
                string payment = Console.ReadLine();

                string policy = RuleList.ElementAt(int.Parse(index) - 1).Key;
                RuleLibrary = RulesEngine.LoadDatabyNode(RuleList, policy);
                foreach (var testkey in RuleLibrary.Keys)
                {
                    Console.WriteLine(RuleLibrary[testkey].ToString());
                }
                
                Console.ReadLine();
            }

            
        }


    }
}
