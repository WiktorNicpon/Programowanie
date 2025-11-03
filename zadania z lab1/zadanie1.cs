using System;

class Program
{
    static double LiczDelta(double a, double b, double c)
    {
        double delta = b * b - 4 * a * c;
        return delta;
    }

    static void ObliczPierwiastki(double a, double b, double c)
    {
        double delta = LiczDelta(a, b, c);
        Console.WriteLine("Delta = " + delta);

        if (delta > 0)
        {
            double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            Console.WriteLine("Dwa pierwiastki: x1 = " + x1 + ", x2 = " + x2);
        }
        else if (delta == 0)
        {
            double x0 = -b / (2 * a);
            Console.WriteLine("Jeden pierwiastek: x0 = " + x0);
        }
        else
        {
            Console.WriteLine("Brak pierwiastków rzeczywistych.");
        }
    }

    static void Main()
    {
        Console.WriteLine("Program oblicza deltę i pierwiastki równania kwadratowego.");
        Console.Write("Podaj a: ");
        double a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Podaj b: ");
        double b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Podaj c: ");
        double c = Convert.ToDouble(Console.ReadLine());

        ObliczPierwiastki(a, b, c);
    }
}