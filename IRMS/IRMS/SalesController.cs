using System;
using System.Collections.Generic;
using System.Text;

namespace IRMS
{
    class SalesController
    {
        private List<SaleItem> currentSaleItems = new List<SaleItem>();
        private List<MenuItem> beefItems = new List<MenuItem>();
        private List<MenuItem> porkItems = new List<MenuItem>();
        private List<MenuItem> chickenItems = new List<MenuItem>();
        private List<MenuItem> drinkItems = new List<MenuItem>();

        public SalesController()
        {
            beefItems.Add(new MenuItem("Beef 1", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 2", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 3", FoodType.BEEF));
            beefItems.Add(new MenuItem("Beef 4", FoodType.BEEF));
            porkItems.Add(new MenuItem("Pork 1", FoodType.PORK));
            porkItems.Add(new MenuItem("Pork 2", FoodType.PORK));
            porkItems.Add(new MenuItem("Pork 3", FoodType.PORK));
            chickenItems.Add(new MenuItem("Chicken 1", FoodType.CHICKEN));
            chickenItems.Add(new MenuItem("Chicken 1", FoodType.CHICKEN));
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
                if(searchItem.menuItem == menuItem)
                {
                    searchItem.quantity++;
                    itemFound = true;
                    break;
                }
            }

            if(!itemFound)
            {
                currentSaleItems.Add(new SaleItem(menuItem));
            }
        }
        public ref List<SaleItem> getCurrentSaleList()
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
    }
}
