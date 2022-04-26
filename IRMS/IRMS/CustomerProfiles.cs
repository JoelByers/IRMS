/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Class for creating and managing customer profiles
 */

using System.Collections.Generic;

namespace IRMS
{
    class CustomerProfiles
    {
        private List<Customer> customerList = new List<Customer>();
        public CustomerProfiles()
        {
            // Customers for testing and demonstraion purposes
            customerList.Add(new Customer("Bill B", "111-222-3333", Rank.BRONZE));
            customerList.Add(new Customer("Bill S", "111-222-3333", Rank.SILVER));
            customerList.Add(new Customer("Bill G", "111-222-3333", Rank.GOLD));
            customerList.Add(new Customer("Bill P", "111-222-3333", Rank.PREMIUM));
        }

        public Customer getCustomer(string name)
        {
            Customer thisCustomer = null;

            foreach (Customer c in customerList)
            {
                if(c.getName() == name)
                {
                    thisCustomer = c;
                    break;
                }
            }

            return thisCustomer;
        }
    }
}
