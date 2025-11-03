using System;

class Licz
{
    private int value;

    public Licz(int start)
    {
        value = start;
    }

    public void Dodaj(int liczba)
    {
        value += liczba;
    }

    public void Odejmij(int liczba)
    {
        value -= liczba;
    }

    public void Wypisz()
    {
        Console.WriteLine($"Wartość: {value}");
    }

    static void Main()
    {
        Licz a = new Licz(10);
        a.Dodaj(5);
        a.Odejmij(3);
        a.Wypisz();

        Licz b = new Licz(100);
        b.Odejmij(50);
        b.Wypisz();
    }
}