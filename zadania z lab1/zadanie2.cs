using System;

class Program
{
    static int[] WczytajLiczby()
    {
        int[] tablica = new int[10];

        Console.WriteLine("Podaj 10 liczb całkowitych:");
        for (int i = 0; i < 10; i++)
        {
            Console.Write("Liczba " + (i + 1) + ": ");
            tablica[i] = Convert.ToInt32(Console.ReadLine());
        }

        return tablica;
    }

    static void PokazWyniki(int[] tablica)
    {
        int suma = 0;
        int iloczyn = 1;
        int min = tablica[0];
        int max = tablica[0];

        for (int i = 0; i < tablica.Length; i++)
        {
            suma = suma + tablica[i];
            iloczyn = iloczyn * tablica[i];

            if (tablica[i] < min)
                min = tablica[i];

            if (tablica[i] > max)
                max = tablica[i];
        }

        double srednia = (double)suma / tablica.Length;

        Console.WriteLine("\nSuma: " + suma);
        Console.WriteLine("Iloczyn: " + iloczyn);
        Console.WriteLine("Średnia: " + srednia);
        Console.WriteLine("Min: " + min);
        Console.WriteLine("Max: " + max);
    }

    static void Main()
    {
        int[] liczby = WczytajLiczby();
        PokazWyniki(liczby);
    }
}