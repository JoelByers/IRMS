using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace IRMS
{
    class SalesController
    {
        private ObservableCollection<SaleItem> currentSaleItems = new ObservableCollection<SaleItem>();
        private List<MenuItem> beefItems = new List<MenuItem>();
        private List<MenuItem> porkItems = new List<MenuItem>();
        private List<MenuItem> chickenItems = new List<MenuItem>();
        private List<MenuItem> drinkItems = new List<MenuItem>();
        private decimal initialCost;
        private decimal totalCost;
        private decimal totalTax;
        private decimal tax;

        public SalesController()
        {
            totalCost = 0;
            totalTax = 0;
            initialCost = 0;
            tax = 0.05M;

            beefItems.Add(new MenuItem("Beef 1", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 2", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 3", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 4", FoodType.BEEF));
            porkItems.Add(new MenuItem("Pork 1", FoodType.PORK));
            porkItems.Add(new MenuItem("Pork 2", FoodType.PORK));
            porkItems.Add(new MenuItem("Pork 3", FoodType.PORK));
            chickenItems.Add(new MenuItem("Chicken 1", FoodType.CHICKEN));
            chickenItems.Add(new MenuItem("Chicken 2", FoodType.CHICKEN));
            drinkItems.Add(new MenuItem("Drink 1", FoodType.DRINK));
            drinkItems.Add(new MenuItem("Drink 2", FoodType.DRINK));
            drinkItems.Add(new MenuItem("Drink 3", FoodType.DRINK));
            drinkItems.Add(new MenuItem("Drink 4", FoodType.DRINK));
            drinkItems.Add(new MenuItem("Drink 5", FoodType.DRINK));
        }

        public void addSaleItem(MenuItem menuItem)
        {
            bool itemFound = false;

            foreach(SaleItem searchItem in currentSaleItems)
            {
                if(searchItem.menuItem.name == menuItem.name)
                {
                    Trace.WriteLine("Found");
                    searchItem.incrementItem();
                    itemFound = true;
                    break;
                }
            }

            if(!itemFound)
            {
                Trace.WriteLine("Add");
                currentSaleItems.Add(new SaleItem(menuItem));
            }

            initialCost += (decimal)menuItem.cost;
            totalTax += Math.Round((decimal)menuItem.cost * tax,2);
            totalCost = initialCost + totalTax;
        }

        public void removeItem(SaleItem item)
        {
            item.decrementItem();
            
            if(item.quantity <= 0)
            {
                currentSaleItems.Remove(item);
            }

            initialCost -= (decimal)item.menuItem.cost;
            totalTax -= Math.Round((decimal)item.menuItem.cost * tax, 2);
            totalCost = initialCost + totalTax;
        }

        public ref ObservableCollection<SaleItem> getCurrentSaleList()
        {
            return ref currentSaleItems;
        }

        public ref List<MenuItem> getBeefItemsList()
        {
            return ref beefItems;
        }

        public ref List<MenuItem> getPorkItemsList()
        {
            return ref porkItems;
        }

        public ref List<MenuItem> getChickenItemsList()
        {
            return ref chickenItems;
        }

        public ref List<MenuItem> getDrinkItemsList()
        {
            return ref drinkItems;
        }

        public decimal getTotalCost()
        {
            return totalCost;
        }

        public decimal getTotalTax()
        {
            return totalTax;
        }
    }
}
