using Shop.Helpers;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public class ProductRepository
    {
        private List<Clothing> products;
        private int idCounter = 1;

        public ProductRepository()
        {
            products = FileService.LoadProducts();
            if (products.Any())
                idCounter = products.Max(p => p.Id) + 1;
            else
            {
                // Dodaje przykładowe produkty, jeśli plik pusty
                AddProduct(new Clothing { Name = "T-Shirt", Price = 50, Amount = 10, Size = "L", Sex = "M", SerialNumber = "TS01" });
                AddProduct(new Clothing { Name = "Jeans", Price = 120, Amount = 5, Size = "32", Sex = "M", SerialNumber = "JN01" });
            }
        }

        public void AddProduct(Clothing product)
        {
            product.Id = idCounter++;
            products.Add(product);
            FileService.SaveProducts(products);
        }

        public IEnumerable<Clothing> GetAllProducts() => products;

        public void SellProduct(int productId, int amount)
        {
            var product = products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.Amount -= amount;
                FileService.SaveProducts(products);
            }
        }
    }
}