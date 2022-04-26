/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Sales Controller class for creating and managing sales
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace IRMS
{
    class SalesController
    {
        private ObservableCollection<SaleItem> currentSaleItems = new ObservableCollection<SaleItem>();
        private ObservableCollection<Coupon> appliedCoupons = new ObservableCollection<Coupon>();
        private List<MenuItem> beefItems = new List<MenuItem>();
        private List<MenuItem> porkItems = new List<MenuItem>();
        private List<MenuItem> chickenItems = new List<MenuItem>();
        private List<MenuItem> drinkItems = new List<MenuItem>();
        private decimal totalDiscount;
        private decimal discountCost;
        private decimal initialCost;
        private decimal totalCost;
        private decimal totalTax;
        private decimal tax;
        private bool isCashSale;

        public SalesController()
        {
            totalCost = 0;
            totalTax = 0;
            initialCost = 0;
            tax = 0.05M;
            totalDiscount = 0;
            discountCost = 0;
            isCashSale = false;

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
            applyCoupons();
            calculateCost();
        }

        public void removeItem(SaleItem item)
        {
            item.decrementItem();
            
            if(item.quantity <= 0)
            {
                currentSaleItems.Remove(item);
            }

            initialCost -= (decimal)item.menuItem.cost;
            applyCoupons();
            calculateCost();
        }

        public void applyCoupons()
        {
            totalDiscount = 0;
            decimal percentTotal = 1;

            // Coupons applying to specific food types
            foreach(Coupon coupon in appliedCoupons)
            {
                if (!coupon.hasFoodType)
                {
                    break;  // skip this coupon
                }

                foreach (SaleItem salesItem in currentSaleItems)
                { 
                    if(coupon.discountFoodType == salesItem.menuItem.foodType)
                    {
                        Trace.WriteLine(salesItem.menuItem.name);
                        totalDiscount += (decimal)salesItem.menuItem.cost * (decimal)salesItem.quantity * coupon.discount;
                    }
                }
            }

            // Coupons applying to total cost
            foreach(Coupon c in appliedCoupons)
            {
                if(!c.hasFoodType)
                {
                    percentTotal -= c.discount;
                }
            }

            totalDiscount += (initialCost - totalDiscount) * (1 - percentTotal);
        }

        public void removeCoupon(Coupon removeCoupon)
        {
            appliedCoupons.Remove(removeCoupon);
            applyCoupons();
            calculateCost();
        }

        public void addCoupon(Coupon newCoupon)
        {
            appliedCoupons.Add(newCoupon);
            applyCoupons();
            calculateCost();
        }

        public void calculateCost()
        {
            discountCost = initialCost - totalDiscount;
            totalTax = Math.Round(discountCost * tax, 2);
            totalCost = discountCost + totalTax;
        }

        public void resetSale()
        {
            totalCost = 0;
            totalTax = 0;
            initialCost = 0;
            tax = 0.05M;
            totalDiscount = 0;
            discountCost = 0;
            isCashSale = false;

            currentSaleItems.Clear();
            appliedCoupons.Clear();
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

        public decimal getTotalDiscount()
        {
            return totalDiscount;
        }

        public decimal getInitialCost()
        {
            return initialCost;
        }

        public ref ObservableCollection<Coupon> getAppliedCoupons()
        {
            return ref appliedCoupons;
        }

        public bool getIsCashSale()
        {
            return isCashSale;
        }

        public void setIsCashSale(bool isCash)
        {
            isCashSale = isCash;
        }
    }
}
