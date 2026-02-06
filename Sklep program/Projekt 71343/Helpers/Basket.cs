using Shop.Models;
using System.Collections.Generic;

namespace Shop.Helpers
{
    public static class Basket
    {
        // Koszyk przechowuje teraz ubrania
        public static List<Clothing> Products { get; private set; } = new List<Clothing>();
        public static int ProductsAmount { get; private set; }
        public static double ProductsCost { get; private set; }

        public static void AddProduct(Clothing product)
        {
            // Kopia, żeby nie modyfikować oryginału w sklepie
            Products.Add(new Clothing(product));
            ProductsAmount += product.Amount;
            ProductsCost += product.Amount * product.Price;
        }

        public static void RemoveProduct(int index)
        {
            var product = Products[index];
            ProductsAmount -= product.Amount;
            ProductsCost -= product.Amount * product.Price;
            Products.RemoveAt(index);
        }

        public static void Clear()
        {
            Products.Clear();
            ProductsAmount = 0;
            ProductsCost = 0;
        }
    }
}