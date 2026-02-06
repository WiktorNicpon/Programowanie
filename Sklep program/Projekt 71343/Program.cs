using Shop.Helpers;
using Shop.Models;
using Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

// Utworzenie katalogu na pliki danych, jeśli nie istnieje
System.IO.Directory.CreateDirectory("Data");

int currentIndex = 0;
bool inProgress = true;
bool clientMenuInProgress = false;
bool sellerMenuInProgress = false;
bool productsListInProgress = false;
bool userBasketInProgress = false;

bool isLoggedIn = false;
string clientPassword = string.Empty;

Client loggedClient = null;

// Inicjalizacja repozytoriów odpowiedzialnych za operacje na plikach
var userRepository = new UserRepository();
var productRepository = new ProductRepository();
var orderRepository = new OrderRepository();

Console.CursorVisible = false;

// Definicje list opcji dla poszczególnych ekranów menu
var mainMenu = new List<string> { "Klient", "Sprzedawca" };
var clientMenu = new List<string> { "Zaloguj", "Zarejestruj", "Wyjście" };
var userMenu = new List<string> { "Koszyk", "Lista produktów", "Moje konto", "Wyloguj" };
var basketMenu = new List<string> { "Usuń z koszyka", "Zatwierdź zakupy", "Wyjście" };

var sellerMenu = new List<string> { "Stan magazynu", "Dostawa towaru", "Zamówienia", "Lista klientów", "Wyjście" };

// Główna pętla aplikacji
while (inProgress)
{
    Console.Clear();
    Console.WriteLine("=== SKLEP ===\n");
    string selectedMain = drawMenu(mainMenu, 5, 5);

    // --- SEKCJA KLIENTA ---
    if (selectedMain == "Klient")
    {
        clientMenuInProgress = true;
        while (clientMenuInProgress)
        {
            Console.Clear();
            Console.WriteLine("=== MENU KLIENTA ===\n");
            string selectedClient = drawMenu(clientMenu, 5, 5);

            if (selectedClient == "Zaloguj")
            {
                Console.Clear();
                Console.WriteLine("=== LOGOWANIE KLIENTA ===\n");
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Hasło: ");
                typePassword(); // Pobieranie hasła z maskowaniem gwiazdkami

                var user = userRepository.GetUserByEmail(email);

                // Weryfikacja: czy użytkownik istnieje, jest typu Client i hasło się zgadza
                if (user != null && user is Client client && user.Password == clientPassword)
                {
                    loggedClient = client;
                    isLoggedIn = true;
                }
                else
                {
                    Console.WriteLine("\n\nBłędne dane lub użytkownik nie jest klientem.");
                    Console.ReadKey();
                }

                clientPassword = string.Empty;

                while (isLoggedIn)
                {
                    Console.Clear();
                    string typeInfo = loggedClient.IsWholesale ? "(KLIENT HURTOWY -10%)" : "(KLIENT DETALICZNY)";
                    Console.WriteLine($"=== PANEL KLIENTA ===\n");
                    Console.WriteLine($"Witaj {loggedClient.FirstName}! {typeInfo}\n");
                    string selectedUser = drawMenu(userMenu, 5, 6);

                    if (selectedUser == "Lista produktów")
                    {
                        productsListInProgress = true;
                        while (productsListInProgress)
                        {
                            Console.Clear();
                            Console.WriteLine("=== KATALOG PRODUKTÓW ===\n");
                            var products = productRepository.GetAllProducts().ToList();

                            if (!products.Any())
                            {
                                Console.WriteLine("Brak produktów.");
                                Console.ReadKey();
                                break;
                            }

                            // Budowanie listy produktów do wyświetlenia w menu
                            var menu = products.Select(p => $"{p.Name} ({p.Size}) | {p.Price} zł").ToList();
                            menu.Add("Wyjście");

                            string chosen = drawMenu(menu, 5, 5);
                            if (chosen == "Wyjście") break;

                            // Szczegóły wybranego produktu
                            var product = products[currentIndex];
                            Console.Clear();
                            Console.WriteLine("=== SZCZEGÓŁY PRODUKTU ===\n");
                            Console.WriteLine($"Nazwa: {product.Name}");
                            Console.WriteLine($"Rozmiar: {product.Size}");
                            Console.WriteLine($"Cena: {product.Price} zł");
                            Console.WriteLine($"Dostępne: {product.Amount} szt.");
                            Console.WriteLine("--------------------------");
                            Console.Write("Podaj ilość do koszyka: ");

                            // Walidacja wprowadzanej ilości
                            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0 && amount <= product.Amount)
                            {
                                var toBasket = new Clothing(product);
                                toBasket.Amount = amount;
                                Basket.AddProduct(toBasket);
                                Console.WriteLine("\nDodano do koszyka.");
                                Console.ReadKey();
                            }
                        }
                    }
                    else if (selectedUser == "Koszyk")
                    {
                        userBasketInProgress = true;
                        while (userBasketInProgress)
                        {
                            viewBasket(productRepository, orderRepository);
                        }
                    }
                    else if (selectedUser == "Moje konto")
                    {
                        Console.Clear();
                        Console.WriteLine("=== MOJE DANE ===\n");

                        printData("Imię:", loggedClient.FirstName);
                        printData("Nazwisko:", loggedClient.LastName);
                        printData("Email:", loggedClient.Email);
                        printData("Adres:", loggedClient.City);
                        printData("Hasło:", loggedClient.Password);

                        Console.WriteLine("-----------------------------");
                        Console.WriteLine($"Portfel: {loggedClient.WalletBalance:F2} zł");
                        Console.WriteLine($"Typ konta: {(loggedClient.IsWholesale ? "Hurtowe (Zniżka 10%)" : "Detaliczne")}");

                        Console.ReadKey();
                    }
                    else if (selectedUser == "Wyloguj")
                    {
                        loggedClient = null;
                        isLoggedIn = false;
                        Basket.Clear();
                    }
                }
            }
            else if (selectedClient == "Zarejestruj")
            {
                // Proces rejestracji nowego klienta
                Console.Clear();
                Console.WriteLine("=== REJESTRACJA NOWEGO KLIENTA ===\n");
                Console.Write("Imię: "); string name = Console.ReadLine();
                Console.Write("Nazwisko: "); string surname = Console.ReadLine();
                Console.Write("Email: "); string email = Console.ReadLine();
                Console.Write("Miasto: "); string city = Console.ReadLine();
                Console.Write("Hasło: "); string pass = Console.ReadLine();

                Console.Write("Czy to konto hurtowe? (t/n): ");
                bool isWholesale = Console.ReadLine().Trim().ToLower() == "t";

                userRepository.AddUser(new Client
                {
                    FirstName = name,
                    LastName = surname,
                    Email = email,
                    City = city,
                    Password = pass,
                    WalletBalance = 1000, // Domyślne saldo początkowe
                    IsWholesale = isWholesale
                });

                Console.WriteLine("\nKonto klienta utworzone.");
                Console.ReadKey();
            }
            else
            {
                clientMenuInProgress = false;
            }
        }
    }
    // --- SEKCJA SPRZEDAWCY ---
    else if (selectedMain == "Sprzedawca")
    {
        Console.Clear();
        Console.WriteLine("=== LOGOWANIE SPRZEDAWCY ===\n");
        Console.WriteLine("(Domyślne: admin@shop.pl / admin)\n");
        Console.Write("Email: "); string email = Console.ReadLine();
        Console.Write("Hasło: "); string pass = Console.ReadLine();

        var user = userRepository.GetUserByEmail(email);

        // Weryfikacja uprawnień sprzedawcy
        if (user != null && user is Seller seller && user.Password == pass)
        {
            sellerMenuInProgress = true;
            while (sellerMenuInProgress)
            {
                Console.Clear();
                Console.WriteLine("=== PANEL SPRZEDAWCY ===\n");
                string selectedSeller = drawMenu(sellerMenu, 5, 5);

                if (selectedSeller == "Stan magazynu")
                {
                    Console.Clear();
                    Console.WriteLine("=== STAN MAGAZYNU ===\n");
                    foreach (var p in productRepository.GetAllProducts())
                    {
                        Console.WriteLine($"ID: {p.Id} | {p.Name} ({p.Size}) | Ilość: {p.Amount} | Cena: {p.Price}");
                    }
                    Console.ReadKey();
                }
                else if (selectedSeller == "Dostawa towaru")
                {
                    // Logika dodawania nowych produktów lub aktualizacji stanu istniejących
                    Console.Clear();
                    Console.WriteLine("=== PRZYJĘCIE TOWARU ===\n");
                    var products = productRepository.GetAllProducts().ToList();
                    foreach (var p in products)
                    {
                        Console.WriteLine($"ID: {p.Id} | {p.Name} ({p.Size}) | Obecnie: {p.Amount}");
                    }
                    Console.WriteLine("---------------------------");
                    Console.Write("Podaj ID produktu do uzupełnienia (lub 0 dla nowego): ");

                    if (int.TryParse(Console.ReadLine(), out int pid))
                    {
                        if (pid == 0)
                        {
                            // Dodawanie zupełnie nowego produktu
                            Console.Write("Nazwa: "); string n = Console.ReadLine();
                            Console.Write("Rozmiar: "); string s = Console.ReadLine();
                            Console.Write("Płeć (M/F): "); string sex = Console.ReadLine();
                            Console.Write("Cena: ");
                            if (!double.TryParse(Console.ReadLine(), out double price)) price = 0;

                            Console.Write("Ilość: ");
                            if (!int.TryParse(Console.ReadLine(), out int amt)) amt = 0;

                            productRepository.AddProduct(new Clothing
                            {
                                Name = n,
                                Size = s,
                                Sex = sex,
                                Price = price,
                                Amount = amt,
                                SerialNumber = Guid.NewGuid().ToString().Substring(0, 6).ToUpper()
                            });
                            Console.WriteLine("\nNowy towar przyjęty na stan.");
                        }
                        else
                        {
                            // Aktualizacja ilości istniejącego produktu
                            var existing = products.FirstOrDefault(x => x.Id == pid);
                            if (existing != null)
                            {
                                Console.Write($"Ile sztuk dodać do '{existing.Name}'? ");
                                if (int.TryParse(Console.ReadLine(), out int addAmt) && addAmt > 0)
                                {
                                    productRepository.SellProduct(existing.Id, -addAmt);
                                    Console.WriteLine("\nStan magazynowy zaktualizowany.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNie znaleziono produktu o takim ID.");
                            }
                        }
                    }
                    Console.ReadKey();
                }
                else if (selectedSeller == "Zamówienia")
                {
                    // Podgląd historii wszystkich zamówień
                    Console.Clear();
                    Console.WriteLine("===== HISTORIA ZAMÓWIEŃ =====\n");

                    var allUsers = userRepository.GetAllUsers().ToList();

                    foreach (var o in orderRepository.GetAllOrders())
                    {
                        var client = allUsers.FirstOrDefault(u => u.Id == o.UserId);
                        string clientInfo = "Nieznany klient";
                        if (client != null)
                        {
                            clientInfo = $"{client.FirstName} {client.LastName} ({client.Email})";
                        }

                        Console.WriteLine($"ZAM. #{o.Id} | {clientInfo}");
                        Console.WriteLine($"KWOTA: {o.TotalPrice:F2} zł | STATUS: {o.Status}");
                        Console.WriteLine(new string('-', 50));
                    }
                    Console.ReadKey();
                }
                else if (selectedSeller == "Lista klientów")
                {
                    // Zarządzanie bazą klientów
                    bool viewingClients = true;
                    while (viewingClients)
                    {
                        Console.Clear();
                        Console.WriteLine("===== LISTA KLIENTÓW =====\n");
                        Console.WriteLine("D - Usunięcie klienta po mailu");
                        Console.WriteLine("Escape - powrót do menu");
                        Console.WriteLine(new string('=', 85));
                        Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-10}", "Imie", "Nazwisko", "Email", "Adres", "Zamówienia");
                        Console.WriteLine(new string('-', 85));

                        var clients = userRepository.GetAllUsers().OfType<Client>().ToList();

                        foreach (var c in clients)
                        {
                            int orderCount = orderRepository.GetAllOrders().Count(o => o.UserId == c.Id);
                            Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-10}",
                                c.FirstName, c.LastName, c.Email, c.City, orderCount);
                        }
                        Console.WriteLine(new string('=', 85));

                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Escape)
                        {
                            viewingClients = false;
                        }
                        else if (key.Key == ConsoleKey.D)
                        {
                            Console.Write("\nPodaj email klienta do usunięcia: ");
                            string mailToDelete = Console.ReadLine();
                            bool removed = userRepository.RemoveUserByEmail(mailToDelete);
                            if (removed) Console.WriteLine("Klient usunięty.");
                            else Console.WriteLine("Nie znaleziono klienta o takim mailu.");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    sellerMenuInProgress = false;
                }
            }
        }
        else
        {
            Console.WriteLine("Błąd logowania sprzedawcy.");
            Console.ReadKey();
        }
    }
}

// Funkcja obsługująca widok koszyka i finalizację zamówienia
void viewBasket(ProductRepository productRepository, OrderRepository orderRepository)
{
    Console.Clear();
    Console.WriteLine("=== TWÓJ KOSZYK ===\n");
    Console.WriteLine($"Liczba produktów: {Basket.ProductsAmount}");

    // Logika naliczania rabatu dla klientów hurtowych
    double finalPrice = Basket.ProductsCost;
    if (loggedClient.IsWholesale)
    {
        finalPrice *= 0.90;
        Console.WriteLine($"Cena katalogowa: {Basket.ProductsCost:F2} zł");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"CENA HURTOWA (-10%): {finalPrice:F2} zł\n");
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine($"Do zapłaty: {finalPrice:F2} zł\n");
    }

    if (!Basket.Products.Any())
    {
        Console.WriteLine("Koszyk pusty.");
        Console.ReadKey();
        userBasketInProgress = false;
        return;
    }

    for (int i = 0; i < Basket.Products.Count; i++)
    {
        var p = Basket.Products[i];
        Console.WriteLine($"{i + 1}. {p.Name} ({p.Size}) x{p.Amount} = {p.Price * p.Amount} zł");
    }

    string selected = drawMenu(basketMenu, 5, 15);

    if (selected == "Usuń z koszyka")
    {
        Console.Write("Nr pozycji: ");
        if (int.TryParse(Console.ReadLine(), out int idx))
        {
            if (idx > 0 && idx <= Basket.Products.Count)
                Basket.RemoveProduct(idx - 1);
        }
    }
    else if (selected == "Zatwierdź zakupy")
    {
        // Sprawdzenie salda w wirtualnym portfelu
        if (loggedClient.WalletBalance < finalPrice)
        {
            Console.WriteLine("Za mało środków w portfelu!");
            Console.ReadKey();
            return;
        }

        // Utworzenie obiektu zamówienia
        var order = new Order
        {
            UserId = loggedClient.Id,
            TotalPrice = finalPrice,
            Status = OrderStatus.Zlozone
        };

        foreach (var p in Basket.Products)
        {
            order.Products.Add(new ProductOrder
            {
                ProductId = p.Id,
                ProductAmount = p.Amount
            });

            productRepository.SellProduct(p.Id, p.Amount);
        }

        loggedClient.WalletBalance -= finalPrice;

        // Aktualizacja pliku z użytkownikami (zapis nowego salda)
        System.IO.File.Delete("Data/users.txt");
        FileService.SaveUsers(userRepository.GetAllUsers().ToList());

        orderRepository.AddOrder(order);
        Basket.Clear();
        Console.WriteLine("Zamówienie złożone. Dziękujemy!");
        Console.ReadKey();
    }
    else
    {
        userBasketInProgress = false;
    }
}

// Funkcja pomocnicza do rysowania menu sterowanego strzałkami
string drawMenu(List<string> items, int x, int y)
{
    while (true)
    {
        Console.SetCursorPosition(0, y);
        // Czyszczenie linii pod menu
        for (int k = 0; k < items.Count * 2; k++) Console.WriteLine(new string(' ', 50));

        int start = y;
        for (int i = 0; i < items.Count; i++)
        {
            // Podświetlenie aktualnie wybranej opcji
            if (i == currentIndex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition(x, start);
            Console.WriteLine(items[i]);
            Console.ResetColor();
            start += 2;
        }

        // Obsługa klawiatury
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.DownArrow) currentIndex = (currentIndex + 1) % items.Count;
        else if (key.Key == ConsoleKey.UpArrow) currentIndex = (currentIndex - 1 + items.Count) % items.Count;
        else if (key.Key == ConsoleKey.Enter)
        {
            var selected = items[currentIndex];
            currentIndex = 0;
            return selected;
        }
    }
}

// Funkcja do wpisywania hasła z maskowaniem
void typePassword()
{
    clientPassword = "";
    while (true)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter) break;
        if (key.Key == ConsoleKey.Backspace && clientPassword.Length > 0)
        {
            clientPassword = clientPassword[..^1];
            Console.Write("\b \b");
        }
        else if (!char.IsControl(key.KeyChar))
        {
            clientPassword += key.KeyChar;
            Console.Write("*");
        }
    }
}

// Funkcja pomocnicza
void printData(string label, string value)
{
    Console.Write($"{label,-15}");
    Console.BackgroundColor = ConsoleColor.Green;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Write(value);
    Console.ResetColor();
    Console.WriteLine("\n");
}