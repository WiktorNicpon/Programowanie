using System;

class Sumator
{
    private int[] Liczby;

    public Sumator(int[] liczby)
    {
        Liczby = liczby;
    }

    public int Suma()
    {
        int suma = 0;
        for (int i = 0; i < Liczby.Length; i++)
            suma += Liczby[i];
        return suma;
    }

    public int SumaPodziel2()
    {
        int suma = 0;
        for (int i = 0; i < Liczby.Length; i++)
            if (Liczby[i] % 2 == 0)
                suma += Liczby[i];
        return suma;
    }

    public int IleElementow()
    {
        return Liczby.Length;
    }

    public void WypiszElementy()
    {
        Console.WriteLine("Elementy tablicy:");
        for (int i = 0; i < Liczby.Length; i++)
            Console.Write(Liczby[i] + " ");
        Console.WriteLine();
    }

    public void WypiszZakres(int lowIndex, int highIndex)
    {
        Console.WriteLine($"Elementy od {lowIndex} do {highIndex}:");
        for (int i = 0; i < Liczby.Length; i++)
        {
            if (i >= lowIndex && i <= highIndex)
                Console.Write(Liczby[i] + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        int[] liczby = { 1, 2, 3, 4, 5, 6 };
        Sumator s = new Sumator(liczby);

        s.WypiszElementy();
        Console.WriteLine($"Suma: {s.Suma()}");
        Console.WriteLine($"Suma podzielnych przez 2: {s.SumaPodziel2()}");
        Console.WriteLine($"Liczba elementów: {s.IleElementow()}");
        s.WypiszZakres(2, 10);
    }
}