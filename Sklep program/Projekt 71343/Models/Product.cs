namespace Shop.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}