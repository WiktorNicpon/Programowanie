namespace Shop.Models
{
    // Klasa abstrakcyjna
    public abstract class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string City { get; set; } = "";
        public string Password { get; set; } = "";

        // RoleType jest teraz właściwością tylko do odczytu, określaną przez klasy dziedziczące
        public abstract RoleType Role { get; }
    }
}