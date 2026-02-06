namespace Shop.Models
{
    public class Seller : User
    {
        // Sprzedawca może mieć np. numer identyfikacyjny pracownika
        public string EmployeeId { get; set; } = "EMP001";

        public override RoleType Role => RoleType.Seller;
    }
}