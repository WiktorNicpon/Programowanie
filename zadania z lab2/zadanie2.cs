using System;

class BankAccount
{
    private decimal saldo;
    public string Wlasciciel { get; private set; }

    public decimal Saldo
    {
        get { return saldo; }
    }

    public BankAccount(string wlasciciel, decimal poczatkoweSaldo)
    {
        Wlasciciel = wlasciciel;
        saldo = poczatkoweSaldo;
    }

    public void Wplata(decimal kwota)
    {
        saldo += kwota;
        Console.WriteLine($"Wpłacono {kwota} zł");
    }

    public void Wyplata(decimal kwota)
    {
        if (kwota <= saldo)
        {
            saldo -= kwota;
            Console.WriteLine($"Wypłacono {kwota} zł");
        }
        else
        {
            Console.WriteLine("Brak środków na koncie!");
        }
    }

    static void Main()
    {
        BankAccount konto = new BankAccount("Jan Kowalski", 1000);
        konto.Wplata(500);
        konto.Wyplata(200);
        Console.WriteLine($"Saldo: {konto.Saldo} zł");
    }
}