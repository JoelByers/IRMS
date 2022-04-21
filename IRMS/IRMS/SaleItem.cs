using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class SaleItem
    {
        public MenuItem menuItem { get; private set; }
        public int quantity 
        {
            get
            {
                return quantity;
            }
            set 
            {
                quantity = value;
                totalCost = menuItem.cost * quantity;
            } 
        }
        public float totalCost 
        {
            get { return totalCost; }
            private set { totalCost = menuItem.cost * quantity; }
        }

        public SaleItem(MenuItem menuItem)
        {
            quantity = 1;
            this.menuItem = menuItem;
        }
    }
}
