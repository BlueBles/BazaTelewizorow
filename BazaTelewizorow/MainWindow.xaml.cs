using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace BazaTelewizorow
{


    public partial class MainWindow : Window
    {
        List<DataClass> dataClass = new List<DataClass>();
        static public int i = 0;
        string path = @"C:\Users\dawid\Desktop\Uczelnia\Zarządzanie jednostką informacyjną\BazaTelewizorow\BazaTelewizorow\plik.txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Losuj_Click(object sender, RoutedEventArgs e) //losowanie 
        {
            if (Text.Text.Length < 1)
                return;

            int ilosc = 0;

            try
            {
                ilosc = int.Parse(Text.Text);
            }
            catch
            {
                MessageBox.Show("nie litery");
            }






            while (ilosc > 0)
            {
                dataClass.Add(new DataClass(++i));
                ilosc--;
            }
            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;

        }
        private void Button_Click(object sender, RoutedEventArgs e) //usuwanie
        {
            if (Text.Text.Length < 1)
                return;
            try //tylko indeks
            {
                int p = int.Parse(NrUsuwania.Text);
            }
            catch
            {
                MessageBox.Show("Nie litery");
                return;
            }
            int numer = int.Parse(NrUsuwania.Text);
            DataClass[] tabela = dataClass.ToArray();
            List<DataClass> dataClassWynik = new List<DataClass>();

            if (numer <= 0)
            {
                return;
            }

            for (int d = 0; d < tabela.Length; d++)
            {
                if (tabela[d].IDelem != numer)
                {
                    dataClassWynik.Add(dataClass[d]);
                }
            }
            dataClass.Clear();
            dataClass = dataClassWynik;

            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;
        }
        private void dodawaniePrzycisk_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int d = int.Parse(RozDodawanie.Text);

                if (d < 0)
                {
                    MessageBox.Show("Nie ujemne");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("Nie litery");
                return;
            }
            try
            {
                int a = int.Parse(NrSeriiDodawanie.Text);

                if (a < 0)
                {
                    MessageBox.Show("Nie ujemne");
                    return;
                }

            }
            catch
            {
                MessageBox.Show("Nie litery");
                return;
            }
            try
            {
                double w = double.Parse(CalDodawanie.Text);

                if (w < 0)
                {
                    MessageBox.Show("Nie ujemne");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Nie litery");
                return;
            }
            try
            {
                double i = double.Parse(CenaDodawanie.Text);
                if (i < 0)
                {
                    MessageBox.Show("Nie ujemne");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Nie litery");
                return;
            }
            string br = MarkaDodawanie.Text;
            string kod = KodDodawanie.Text;
            int rozdz = int.Parse(RozDodawanie.Text);
            int nrserii = int.Parse(NrSeriiDodawanie.Text);
            bool sprze = sprzedaz.IsChecked ?? true;
            double cal1 = double.Parse(CalDodawanie.Text);
            double cena1 = double.Parse(CenaDodawanie.Text);
            string opis1 = OpisDodawanie.Text;

            dataClass.Add(new DataClass(++i, br, kod, rozdz, nrserii, sprze, cal1, cena1, opis1));

            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;
        }
        private void wczytaj_Click(object sender, RoutedEventArgs e)
        {

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    string[] lineWords = line.Split('`');
                    dataClass.Add(new DataClass(int.Parse(lineWords[0]), lineWords[1], lineWords[2], int.Parse(lineWords[3]), int.Parse(lineWords[4]), bool.Parse(lineWords[5]), double.Parse(lineWords[6]), double.Parse(lineWords[7]), lineWords[8]));

                }




                sr.Close();
                Tabela.ItemsSource = null;
                Tabela.ItemsSource = dataClass;
                Tabela.DataContext = dataClass;
            }
        }
        private void zapisz_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sr = new StreamWriter(path))
            {
                foreach (var i in dataClass)
                {
                    sr.Write(i.IDelem);
                    sr.Write("`");
                    sr.Write(i.brand);
                    sr.Write("`");
                    sr.Write(i.kodproduktu);
                    sr.Write("`");
                    sr.Write(i.rozdzielczosc);
                    sr.Write("`");
                    sr.Write(i.nrSerii);
                    sr.Write("`");
                    sr.Write(i.sprzedaz);
                    sr.Write("`");
                    sr.Write(i.cal);
                    sr.Write("`");
                    sr.Write(i.cena);
                    sr.Write("`");
                    sr.Write(i.opis);
                    sr.Write("\n");

                }


                sr.Close();

            }
        }
        private void kasujwszystko_Click(object sender, RoutedEventArgs e)
        {



            for (int g = 0; g < dataClass.Count();)
            {
                dataClass.Remove(dataClass[g]);
            }
            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;

            i = 0;
        }
        private void SzukaniePrzycisk_Click(object sender, RoutedEventArgs e)
        {

            if (KodSzukanie.Text == "" || RozSzukanie.Text == "" || CalSzukanie.Text == "" || CenaSzukanie.Text == "")
                return;


            string br = MarkaSzukanie.Text;
            string kod = KodSzukanie.Text;
            int rozdz = int.Parse(RozSzukanie.Text);
            int nrserii = int.Parse(NrSeriiSzukanie.Text);
            double cal1 = double.Parse(CalSzukanie.Text);
            double cena1 = double.Parse(CenaSzukanie.Text);
            string opis1 = OpisSzukanie.Text;


            DataClass[] tabela = dataClass.ToArray();
            List<DataClass> dataClassWynik = new List<DataClass>();
            var timer = System.Diagnostics.Stopwatch.StartNew(); //licznik czasu
            if (br.Length >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);
                sortowanie.sortowanieBrandSelectionSort();
                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneMarkaString(MarkaSzukanie.Text));

            }
            if (kod.Length >= 2)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieKodSelectionSort();

                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneKodString(KodSzukanie.Text));
            }
            if (rozdz >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieRozSelectionSort();

                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneRoz(rozdz));

            }
            if (nrserii >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieNrSeriiSelectionSort();

                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneInt(int.Parse(NrSeriiSzukanie.Text)));

            }
            if (cal1 >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieCalSelectionSort();
                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneCal(cal1));
            }
            if (cena1 >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieCenaSelectionSort();

                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneCena(cena1));
            }
            if (opis1.Length >= 1)
            {
                Sortowanie sortowanie = new Sortowanie(dataClass);

                sortowanie.sortowanieOpisSelectionSort();

                dataClass = sortowanie.zwrot().ToList();
                Wyszukiwanie wyszukiwanie = new Wyszukiwanie(dataClass);
                dataClassWynik.Add(wyszukiwanie.binarneOpisString(opis1));
            }
            timer.Stop();
            MessageBox.Show("Czas szukania binarnego to mili -> " + timer.ElapsedMilliseconds + "\nIlość ticków -> " + timer.ElapsedTicks
                + "\nCzas w nanosekundach -> " + timer.ElapsedTicks * 1000000000 / System.Diagnostics.Stopwatch.Frequency
                ); //czas wykonywania -> komunikat

            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClassWynik;
            Tabela.DataContext = dataClassWynik;
        }
        private void wrocSzukanie_Click(object sender, RoutedEventArgs e)
        {
            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;
        }
        private void sortuj_Click(object sender, RoutedEventArgs e)
        {
            if (dataClass.Count() == 0)
                return;

            var timer = System.Diagnostics.Stopwatch.StartNew();

            Sortowanie sortowanie = new Sortowanie(dataClass);

            sortowanie.sortowanieNrSeriiSelectionSort();

            dataClass = sortowanie.zwrot().ToList();

            timer.Stop();

            MessageBox.Show("czas milisekundach = " + timer.ElapsedMilliseconds + "\nIlość ticków -> " + timer.ElapsedTicks
                + "\nCzas w nanosekundach -> " + timer.ElapsedTicks * 1000000000 / System.Diagnostics.Stopwatch.Frequency);


            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;
        }
        private void sortujString_Click(object sender, RoutedEventArgs e)
        {
            if (dataClass.Count() == 0)
                return;

            var timer = System.Diagnostics.Stopwatch.StartNew();

            Sortowanie sortowanie = new Sortowanie(dataClass);

            sortowanie.sortowanieBrandSelectionSort();

            dataClass = sortowanie.zwrot().ToList();

            timer.Stop();

            MessageBox.Show("czas milisekundach = " + timer.ElapsedMilliseconds + "\nIlość ticków -> " + timer.ElapsedTicks
                + "\nCzas w nanosekundach -> " + timer.ElapsedTicks * 1000000000 / System.Diagnostics.Stopwatch.Frequency);


            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClass;
            Tabela.DataContext = dataClass;
        }
        private void HashPrzycisk_Click(object sender, RoutedEventArgs e) //tablica hashowa
        {
            List<DataClass> wynik = new List<DataClass>(); //wynik ktory zostanie wyswietlony
            Hashtable tablicahashowa = new Hashtable(dataClass.Count()); //tworzenie tablicyhashowej
            int i = 0; //unikalny klucz taki sam jak ID
            foreach (var t in dataClass) //dodanie dataclass do tablicyhashowej
            {
                tablicahashowa.Add(dataClass[i].IDelem, dataClass[i].brand);
                i++;
            }
            var timer = System.Diagnostics.Stopwatch.StartNew(); //licznik czasu
            if (tablicahashowa.ContainsValue(hashtable.Text) == true) //sprawdzanie czy zawiera
            {
                foreach (DictionaryEntry t in tablicahashowa)
                {
                    if ((string)t.Value == hashtable.Text)
                    {
                        wynik.Add(dataClass[((int)t.Key) - 1]); //dodawanie prawidlowych
                    }
                }
            }
            timer.Stop();
            MessageBox.Show("Czas szukania binarnego to mili -> " + timer.ElapsedMilliseconds + "\nIlość ticków -> " + timer.ElapsedTicks
                + "\nCzas w nanosekundach -> " + timer.ElapsedTicks * 1000000000 / System.Diagnostics.Stopwatch.Frequency
                ); //czas wykonywania -> komunikat
            Tabela.ItemsSource = null; //wyswietlanie 
            Tabela.ItemsSource = wynik;
            Tabela.DataContext = wynik;
        }
        private void najczesciej_Click(object sender, RoutedEventArgs e)
        {

            using (StreamReader inputFile = new StreamReader("marka.txt"))
            {
                string najwiecejMarka = "brak";
                int najwiecej = 0;

                string marka;
                for (int i = 1; i < 26; i++)
                {
                    int ile = 0;
                    marka = inputFile.ReadLine();
                    foreach (var t in dataClass)
                    {
                        if (t.brand == marka)
                        {
                            ile++;
                        }
                    }
                    if (ile > najwiecej)
                    {
                        najwiecej = ile;
                        najwiecejMarka = marka;
                    }

                }
                MessageBox.Show(najwiecejMarka);


            }

        }

        private void SzukanieLiniowePrzycisk_Click(object sender, RoutedEventArgs e)
        {

            if (KodSzukanie.Text == "" || RozSzukanie.Text == "" || CalSzukanie.Text == "" || CenaSzukanie.Text == "")
                return;


            string br = MarkaSzukanie.Text;
            string kod = KodSzukanie.Text;
            int rozdz = int.Parse(RozSzukanie.Text);
            int nrserii = int.Parse(NrSeriiSzukanie.Text);
            double cal1 = double.Parse(CalSzukanie.Text);
            double cena1 = double.Parse(CenaSzukanie.Text);
            string opis1 = OpisSzukanie.Text;


            DataClass[] tabela = dataClass.ToArray();

            List<DataClass> dataClassWynik = new List<DataClass>();
            var timer = System.Diagnostics.Stopwatch.StartNew();
            if (br.Length >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.brand == br.ToUpper())
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (kod.Length >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.kodproduktu == kod.ToUpper())
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (rozdz >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.rozdzielczosc == rozdz)
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (nrserii >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.nrSerii == nrserii)
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (cal1 >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.cal == cal1)
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (cena1 >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.cena == cena1)
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            if (opis1.Length >= 1)
            {
                foreach (var t in tabela)
                {
                    if (t.opis == opis1)
                    {
                        dataClassWynik.Add(t);
                    }
                }
            }
            timer.Stop();
            MessageBox.Show("Czas szukania binarnego to mili -> " + timer.ElapsedMilliseconds + "\nIlość ticków -> " + timer.ElapsedTicks
                + "\nCzas w nanosekundach -> " + timer.ElapsedTicks * 1000000000 / System.Diagnostics.Stopwatch.Frequency
                ); //czas wykonywania -> komunikat
            Tabela.ItemsSource = null;
            Tabela.ItemsSource = dataClassWynik;
            Tabela.DataContext = dataClassWynik;
        }

    }
}
