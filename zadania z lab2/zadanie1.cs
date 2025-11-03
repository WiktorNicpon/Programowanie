using System;

class Osoba
{
    private string imie;
    private string nazwisko;
    private int wiek;

    public Osoba(string imie, string nazwisko, int wiek)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        Wiek = wiek;
    }

    public string Imie
    {
        get { return imie; }
        set
        {
            if (value.Length >= 2)
                imie = value;
            else
                Console.WriteLine("Imię musi mieć co najmniej 2 znaki!");
        }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set
        {
            if (value.Length >= 2)
                nazwisko = value;
            else
                Console.WriteLine("Nazwisko musi mieć co najmniej 2 znaki!");
        }
    }

    public int Wiek
    {
        get { return wiek; }
        set
        {
            if (value > 0)
                wiek = value;
            else
                Console.WriteLine("Wiek musi być dodatni!");
        }
    }

    public void WyswietlInformacje()
    {
        Console.WriteLine($"Imię: {imie}, Nazwisko: {nazwisko}, Wiek: {wiek}");
    }

    static void Main()
    {
        Osoba osoba1 = new Osoba("Jan", "Kowalski", 25);
        osoba1.WyswietlInformacje();
    }
}