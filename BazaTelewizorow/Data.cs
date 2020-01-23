using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BazaTelewizorow
{
    public class Losowator
    {
        static Random random = new Random(Guid.NewGuid().GetHashCode()); //losowator o takim seedzie że łohohoho
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomBrand() // losowanie marki z puli jaka jest w pliku marka.txt
        {
            var randBrand = random.Next(0, 26);
            using (StreamReader inputFile = new StreamReader("marka.txt"))
            {
                for (int i = 1; i < randBrand; i++)
                {
                    inputFile.ReadLine();
                }

                return inputFile.ReadLine();
            }
        }
        public static int RandomRozdzielczosc() //losowa rozdzielczosc z puli
        {
            int[] rozdzielczosc = { 1280, 2560, 1920, 1680, 2560, 3440, 4096, 480, 1440 };
            var randBrand = random.Next(0, rozdzielczosc.Length);
            return rozdzielczosc[randBrand];
        }
        public static bool RandomSprzedaz() //losowy boolean true/false
        {
            if (random.Next(0, 2) == 1)
            {
                return true;
            }
            else { return false; }
        }
        public static string RandomSeria(int length) //losowa seria
        {

            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomStringOpis() //losowy opis łączy kilka wyrazów z pul
        {
            string zdanie = "Telewizor jest ";
            string[] wyraz = { "super", "czarny", "biały", "elegancki", "ekstra", "srebrny" };
            zdanie += wyraz[random.Next(0, wyraz.Length)];
            return zdanie;
        }
        public static string RandomCal() //losowa ilość cali
        {

            const string chars = "0123456789";
            return (new string(Enumerable.Repeat(chars, 2)
              .Select(s => s[random.Next(s.Length)]).ToArray())
                + "," + new string(Enumerable.Repeat(chars, 2)
              .Select(s => s[random.Next(s.Length)]).ToArray()));
        }
        public static string RandomCena()//losowa cena
        {

            const string chars = "0123456789";
            return (new string(Enumerable.Repeat(chars, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray())
                + "," + new string(Enumerable.Repeat(chars, 2)
              .Select(s => s[random.Next(s.Length)]).ToArray()));
        }
    }
    public class DataClass //jednostka danych
    {
        //wszystkie wartości jakie posiada DataClass z możliwością odczytu i zapisu
        public int IDelem { get; set; }
        public string brand { get; set; }
        public string kodproduktu { get; set; }
        public int rozdzielczosc { get; set; }
        public int nrSerii { get; set; }
        public bool sprzedaz { get; set; }
        public double cal { get; set; }
        public double cena { get; set; }
        public string opis { get; set; }

        public DataClass(string brak = "brak") //używać ostrożnie
        {
            IDelem = 0;

            kodproduktu = brak;
            brand = brak;
            rozdzielczosc = 0;
            nrSerii = 0;
            sprzedaz = false;
            opis = brak;
            cal = 0;
            cena = 0;
        }
        public DataClass(int id) //inicjalizacja i losowanie
        {
            IDelem = id;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            kodproduktu = Losowator.RandomString(10);
            brand = Losowator.RandomBrand();
            rozdzielczosc = Losowator.RandomRozdzielczosc();
            sprzedaz = Losowator.RandomSprzedaz();
            opis = Losowator.RandomStringOpis();
            cal = double.Parse(Losowator.RandomCal());
            cena = double.Parse(Losowator.RandomCena());
            nrSerii = int.Parse(Losowator.RandomSeria(5));
        }
        public DataClass(
        int id = 0,
        string br = "",
        string kod = "",
        int rozdz = 0,
        int nrserii = 0,
        bool sprze = false,
        double cal1 = 0,
        double cena1 = 0,
        string opis1 = "") //używany przy dodawaniu elementów, jeżeli jakiś nie zostanie wypełniony, przyjmie wartość startową
        {
            this.IDelem = id;
            this.brand = br;
            this.kodproduktu = kod;
            this.rozdzielczosc = rozdz < 0 ? 0 : rozdz;
            this.nrSerii = nrserii < 0 ? 0 : nrserii;
            this.sprzedaz = sprze;
            this.cal = cal1 < 0 ? 0 : cal1;
            this.cena = cena1 < 0 ? 0 : cena1;
            this.opis = opis1;
        }

        public DataClass()
        {

        }

    }
    public class Sortowanie
    {
        DataClass[] tablica;
        public Sortowanie(List<DataClass> tablica)
        {
            this.tablica = tablica.ToArray();
        }
        //wyszukiwanie binarne po NRserii, więc sortowanie po nrserii
        public void sortowanieNrSeriiBubbleSort() //sortowanie bąbelkowe
        {
            for (int p = 0; p < tablica.Length; p++)
            {
                for (int q = 1; q < tablica.Length; q++)
                {
                    if (tablica[q - 1].nrSerii > tablica[q].nrSerii)
                    {
                        Swap(q - 1, q);
                    }
                }
            }

        }
        public void Swap(int first, int second)//zamiana miejscami w tablicy
        {
            var temporary = tablica[first];
            tablica[first] = tablica[second];
            tablica[second] = temporary;
        }
        public void sortowanieNrSeriiSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[index].nrSerii < tablica[smallest].nrSerii)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);
            }
        }
        public void sortowanieKodProduktuSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[smallest].kodproduktu.CompareTo(tablica[index].kodproduktu) > 0)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);
            }
        }

        public void sortowanieBrandSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[smallest].brand.CompareTo(tablica[index].brand) > 0)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);
            }
        }
        public void sortowanieOpisSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[smallest].opis.CompareTo(tablica[index].opis) > 0)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);
            }
        }
        public void sortowanieKodSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[smallest].kodproduktu.CompareTo(tablica[index].kodproduktu) > 0)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);
            }
        }
        public void sortowanieRozSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[index].rozdzielczosc < tablica[smallest].rozdzielczosc)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);


            }
        }
        public void sortowanieCalSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[index].cal < tablica[smallest].cal)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);


            }
        }
        public void sortowanieCenaSelectionSort()//sortowanie przez selekcje
        {
            int smallest;
            for (int i = 0; i < tablica.Length - 1; i++)
            {
                smallest = i;

                for (int index = i + 1; index < tablica.Length; index++)
                {
                    if (tablica[index].cena < tablica[smallest].cena)
                    {
                        smallest = index;
                    }
                }
                Swap(i, smallest);


            }
        }
        public DataClass[] zwrot() //pobranie tablicy z klasy sortowej
        {
            return this.tablica;
        }
    }
    public class Wyszukiwanie
    {
        static string brak = "brak"; //gdyby nie znalazło
        DataClass[] tablica;
        DataClass wynik = new DataClass(brak);
        List<DataClass> wynikList = new List<DataClass>();
        public Wyszukiwanie(List<DataClass> tablica)//wpisanie listy do tablicy
        {
            this.tablica = tablica.ToArray();
        }
        public List<DataClass> liniowe(int szukana)//wyszukiwanie liniowe
        {
            //int d = 0;
            foreach (var t in tablica)
            {
                //d++;
                if (t.nrSerii == szukana)
                {
                    wynikList.Add(t);
                    //System.Console.WriteLine("moje d " + d);
                    return wynikList;
                }
            }

            return wynikList;
        }
        public DataClass binarneInt(int szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].nrSerii == szukana)
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].nrSerii > szukana)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;



        }
        public DataClass binarneRoz(int szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].rozdzielczosc == szukana)
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].rozdzielczosc > szukana)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;



        }
        public DataClass binarneCal(double szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].cal == szukana)
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].cal > szukana)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;



        }
        public DataClass binarneCena(double szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].cena == szukana)
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].cena > szukana)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;



        }
        public DataClass binarneMarkaString(string szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].brand.Equals(szukana))
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].brand.CompareTo(szukana) > 0)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;
        }
        public DataClass binarneOpisString(string szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].opis.Equals(szukana))
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].opis.CompareTo(szukana) > 0)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;
        }
        public DataClass binarneKodString(string szukana) //wyszukiwanie binarne
        {
            int l, p;
            l = 0;
            p = tablica.Length - 1;
            int sr;
            while (l <= p)
            {
                sr = (l + p) / 2;
                if (tablica[sr].kodproduktu.Equals(szukana))
                {
                    wynik = tablica[sr];
                    return wynik;
                }
                if (tablica[sr].kodproduktu.CompareTo(szukana) > 0)
                    p = sr - 1;
                else
                    l = sr + 1;
            }
            return wynik;
        }

    }

   

}