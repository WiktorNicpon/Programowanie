using System.Collections.Generic;

namespace Shop.Models
{
    public class Client : User
    {
        public double WalletBalance { get; set; } = 1000;

        // Nowe pole dla klienta hurtowego/stałego
        public bool IsWholesale { get; set; } = false;

        public List<Order> Orders { get; set; } = new();

        public override RoleType Role => RoleType.Client;
    }
}