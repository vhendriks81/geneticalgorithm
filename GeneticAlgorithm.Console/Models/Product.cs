namespace GeneticAlgorithm.Console.Models
{
    public class Product
    {
        public Product(int weight, int price)
        {
            Weight = weight;
            Price = price;
        }

        public int Weight { get; }

        public int Price { get; }

        public override string ToString()
        {
            return $"{Price} ({Weight}g)";
        }
    }
}
