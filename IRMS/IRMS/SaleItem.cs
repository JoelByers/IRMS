using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class SaleItem
    {
        public MenuItem menuItem { get; set; }
        public string item { get; set; }
        public int quantity { get; set; }
        public float totalCost { get; set; }

        public SaleItem(MenuItem menuItem)
        {
            quantity = 1;
            this.menuItem = menuItem;
            totalCost = quantity * menuItem.cost;
            this.item = menuItem.name;
        }

        public void incrementItem()
        {
            quantity++;
            totalCost = quantity * menuItem.cost;
        }
        public void decrementItem()
        {
            quantity--;
            totalCost = quantity * menuItem.cost;
        }
    }
}
