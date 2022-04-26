/*  SE 306
 *  Prestige Worldwide
 *  
 *  Description: Menu Item data type
 */


namespace IRMS
{
    public enum FoodType { BEEF, PORK, CHICKEN, DRINK }

    class MenuItem
    {
        public string name { get; private set; }
        public FoodType foodType { get; private set; }
        public float cost { get; set; }

        public MenuItem(string name, FoodType type)
        {
            this.name = name;
            this.foodType = type;

            switch (foodType)
            {
                case FoodType.BEEF:
                    cost = 15;
                    break;
                case FoodType.PORK:
                    cost = 12;
                    break;
                case FoodType.CHICKEN:
                    cost = 10;
                    break;
                case FoodType.DRINK:
                    cost = 7;
                    break;
                default:
                    cost = -1;
                    break;
            }
        }
    }
}
