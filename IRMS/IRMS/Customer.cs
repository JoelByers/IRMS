using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    public enum Rank {BRONZE, SILVER, GOLD, PREMIUM}
    class Customer
    {
        private string name;
        private string phoneNumber;
        private Rank rank;

        public Customer(string name, string phoneNumber, Rank rank)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.rank = rank;
        }

        public string getName()
        {
            return name;
        }

        public Rank getRank()
        {
            return rank;
        }
    }
}
