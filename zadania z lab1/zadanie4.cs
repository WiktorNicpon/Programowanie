using System;

class Program
{
    static void PytajOLiczby()
    {
        while (true)
        {
            Console.Write("Podaj liczbę całkowitą: ");
            int liczba = Convert.ToInt32(Console.ReadLine());

            if (liczba < 0)
            {
                Console.WriteLine("Podano liczbę ujemną, koniec programu.");
                break;
            }

            Console.WriteLine("Wprowadziłeś: " + liczba);
        }
    }

    static void Main()
    {
        PytajOLiczby();
    }
}