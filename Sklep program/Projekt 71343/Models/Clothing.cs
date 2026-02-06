namespace Shop.Models
{
    public class Clothing : Product
    {
        public string Size { get; set; }
        public string Sex { get; set; }

        // Konstruktor bezparametrowy
        public Clothing() { }

        // Konstruktor kopiujący
        public Clothing(Clothing copy)
        {
            Id = copy.Id;
            SerialNumber = copy.SerialNumber;
            Name = copy.Name;
            Amount = copy.Amount;
            Price = copy.Price;
            Size = copy.Size;
            Sex = copy.Sex;
        }
    }
}