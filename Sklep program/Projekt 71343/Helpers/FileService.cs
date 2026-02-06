using Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Helpers
{
    public static class FileService
    {
        private const string UsersFile = "Data/users.txt";
        private const string ProductsFile = "Data/products.txt";
        private const string OrdersFile = "Data/orders.txt";

        // === USERS ===
        public static void SaveUsers(List<User> users)
        {
            var lines = new List<string>();
            foreach (var u in users)
            {
                // Podstawa: Typ;Id;Imie;Nazwisko;Email;Miasto;Haslo
                string line = $"{u.GetType().Name};{u.Id};{u.FirstName};{u.LastName};{u.Email};{u.City};{u.Password}";

                if (u is Client c)
                {
                    // Dla klienta dochodzi: Portfel;CzyHurtownik
                    line += $";{c.WalletBalance};{c.IsWholesale}";
                }

                lines.Add(line);
            }
            File.WriteAllLines(UsersFile, lines);
        }

        public static List<User> LoadUsers()
        {
            if (!File.Exists(UsersFile)) return new List<User>();

            var users = new List<User>();
            var lines = File.ReadAllLines(UsersFile);

            foreach (var line in lines)
            {
                var p = line.Split(';');
                if (p.Length < 7) continue; // Zabezpieczenie przed pustymi liniami

                var type = p[0];

                if (type == nameof(Client))
                {
                    var client = new Client
                    {
                        Id = int.Parse(p[1]),
                        FirstName = p[2],
                        LastName = p[3],
                        Email = p[4],
                        City = p[5],
                        Password = p[6],
                        WalletBalance = double.Parse(p[7])
                    };

                    // Obsługa nowego pola
                    if (p.Length > 8)
                    {
                        client.IsWholesale = bool.Parse(p[8]);
                    }

                    users.Add(client);
                }
                else if (type == nameof(Seller))
                {
                    users.Add(new Seller
                    {
                        Id = int.Parse(p[1]),
                        FirstName = p[2],
                        LastName = p[3],
                        Email = p[4],
                        City = p[5],
                        Password = p[6]
                    });
                }
            }
            return users;
        }

        //PRODUCTS
        public static void SaveProducts(List<Clothing> products)
        {
            File.WriteAllLines(ProductsFile, products.Select(p =>
                $"{p.Id};{p.SerialNumber};{p.Name};{p.Amount};{p.Price};{p.Size};{p.Sex}"));
        }

        public static List<Clothing> LoadProducts()
        {
            if (!File.Exists(ProductsFile)) return new List<Clothing>();

            return File.ReadAllLines(ProductsFile).Select(l =>
            {
                var p = l.Split(';');
                return new Clothing
                {
                    Id = int.Parse(p[0]),
                    SerialNumber = p[1],
                    Name = p[2],
                    Amount = int.Parse(p[3]),
                    Price = double.Parse(p[4]),
                    Size = p[5],
                    Sex = p[6]
                };
            }).ToList();
        }

        //ORDERS
        public static void SaveOrders(List<Order> orders)
        {
            var lines = new List<string>();
            foreach (var o in orders)
            {
                string productsString = string.Join(",", o.Products.Select(po => $"{po.ProductId}:{po.ProductAmount}"));
                lines.Add($"{o.Id};{o.UserId};{o.TotalPrice};{o.Status};{productsString}");
            }
            File.WriteAllLines(OrdersFile, lines);
        }

        public static List<Order> LoadOrders()
        {
            if (!File.Exists(OrdersFile)) return new List<Order>();

            var list = new List<Order>();
            foreach (var line in File.ReadAllLines(OrdersFile))
            {
                var p = line.Split(';');
                var order = new Order
                {
                    Id = int.Parse(p[0]),
                    UserId = int.Parse(p[1]),
                    TotalPrice = double.Parse(p[2]),
                    Status = Enum.Parse<OrderStatus>(p[3])
                };

                if (p.Length > 4 && !string.IsNullOrEmpty(p[4]))
                {
                    var prodItems = p[4].Split(',');
                    foreach (var item in prodItems)
                    {
                        var parts = item.Split(':');
                        order.Products.Add(new ProductOrder
                        {
                            ProductId = int.Parse(parts[0]),
                            ProductAmount = int.Parse(parts[1])
                        });
                    }
                }
                list.Add(order);
            }
            return list;
        }
    }
}