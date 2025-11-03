using System;

class Program
{
    static int[] WczytajLiczby(int n)
    {
        int[] tab = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write("Podaj liczbę " + (i + 1) + ": ");
            tab[i] = Convert.ToInt32(Console.ReadLine());
        }
        return tab;
    }

    static void SortujBabelkowo(int[] tab)
    {
        for (int i = 0; i < tab.Length - 1; i++)
        {
            for (int j = 0; j < tab.Length - 1 - i; j++)
            {
                if (tab[j] > tab[j + 1])
                {
                    int temp = tab[j];
                    tab[j] = tab[j + 1];
                    tab[j + 1] = temp;
                }
            }
        }
    }

    static void Main()
    {
        Console.Write("Podaj, ile liczb chcesz wprowadzić: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] liczby = WczytajLiczby(n);

        Console.WriteLine("\nLiczby przed sortowaniem:");
        for (int i = 0; i < liczby.Length; i++)
        {
            Console.Write(liczby[i] + " ");
        }

        SortujBabelkowo(liczby);

        Console.WriteLine("\n\nLiczby po sortowaniu:");
        for (int i = 0; i < liczby.Length; i++)
        {
            Console.Write(liczby[i] + " ");
        }
    }
}