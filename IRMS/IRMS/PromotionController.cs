using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IRMS
{
    class PromotionController
    {
        private ObservableCollection<Coupon> couponList = new ObservableCollection<Coupon>();

        public PromotionController()
        {
            couponList.Add(new Coupon("5% Off Meal", 0.05M));
            couponList.Add(new Coupon("10% Off Meal", 0.1M));
            couponList.Add(new Coupon("5% off Beef", 0.1M, FoodType.BEEF));
            couponList.Add(new Coupon("5% off Pork", 0.05M, FoodType.PORK));
            couponList.Add(new Coupon("5% off Chicken", 0.05M, FoodType.CHICKEN));
            couponList.Add(new Coupon("5% off Drinks", 0.05M, FoodType.DRINK));

        }

        public ref ObservableCollection<Coupon> getCouponList()
        {
            return ref couponList;
        }
    }
}
