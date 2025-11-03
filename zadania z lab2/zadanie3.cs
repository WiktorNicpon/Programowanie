using System;
using System.Linq;

class Student
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    private int[] oceny;
    private int liczbaOcen;

    public Student(string imie, string nazwisko)
    {
        Imie = imie;
        Nazwisko = nazwisko;
        oceny = new int[100];
        liczbaOcen = 0;
    }

    public void DodajOcene(int ocena)
    {
        oceny[liczbaOcen] = ocena;
        liczbaOcen++;
    }

    public double SredniaOcen
    {
        get
        {
            if (liczbaOcen == 0)
                return 0;
            int suma = 0;
            for (int i = 0; i < liczbaOcen; i++)
                suma += oceny[i];
            return (double)suma / liczbaOcen;
        }
    }

    public void Wyswietl()
    {
        Console.WriteLine($"Student: {Imie} {Nazwisko}, Średnia ocen: {SredniaOcen:F2}");
    }

    static void Main()
    {
        Student s1 = new Student("Anna", "Nowak");
        s1.DodajOcene(5);
        s1.DodajOcene(4);
        s1.DodajOcene(3);
        s1.Wyswietl();
    }
}