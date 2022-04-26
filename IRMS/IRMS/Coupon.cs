/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Coupon Data Type
 */

namespace IRMS
{
    class Coupon
    {
        public string description { get; private set; }
        public decimal discount { get; private set; }
        public FoodType discountFoodType { get; private set; }
        public bool hasFoodType { get; private set; }
        
        public Coupon(string description, decimal discount)
        {
            this.description = description;
            this.discount = discount;
            this.discountFoodType = FoodType.BEEF;
            hasFoodType = false;
        }

        public Coupon(string description, decimal discount, FoodType foodType)
        {
            this.description = description;
            this.discount = discount;
            this.discountFoodType = foodType;
            hasFoodType = true;
        }
    }
}
