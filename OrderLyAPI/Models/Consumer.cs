namespace OrderLy_API.Models
{
    public class Consumer
    {
        public string Name { get; set; } = null!;
        public FoodItem[] FoodItems { get; set; } = [];
        public double MoneyOwed { get; set; } = 0.0;
        public double MoneyReturn { get; set; } = 0.0;

        public Consumer()
        {
            CalcCost();
        }

        public void CalcCost()
        {
            foreach (var item in FoodItems)
            {
                MoneyOwed += item.Price;
            }
        }
    }
}
