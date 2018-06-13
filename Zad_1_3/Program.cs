using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;





namespace Pw_1_1
{
    class Program
    {

        public static void Main(string[] args)
        {
            int liczba_wierz = 1000;
            int[,] graf = CreateGra(liczba_wierz, 100);
            int[] suma = new int[liczba_wierz];

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Liczba krawedzi: " + Licz_sek(graf));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Czas 1: " + elapsedMs);


            watch = System.Diagnostics.Stopwatch.StartNew();
            Licz_watki(graf, graf.GetLength(0), suma);

            Console.WriteLine("Liczba krawedzi: " + suma.Sum());
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Czas 2: " + elapsedMs);

            Console.ReadKey();
        }

        public static int[,] CreateGra(int a, int b)
        {
            int[,] graf = new int[a, a];
            Random rnd = new Random();

            for (int i = 0; i < a; i++)
            {
                for (int j = i + 1; j < a; j++)
                {
                    if (rnd.Next(0, 101) <= b)
                    {
                        graf[i, j] = 1;
                    }
                    else
                    {
                        graf[i, j] = 0;
                    }
                }
            }



            return graf;
        }
        public static int Licz_sek(int[,] a)
        {
            int k = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == 1)
                        k++;
                }
            }
            return k;
        }



        public static void Licz_watki(int[,] a, int b, int[] kk)
        {
            Thread[] threads = new Thread[b];
            for (int i = 0; i < b; i++)
            {

                Thread t = new Thread(() => Licz(a, i, ref kk));
                t.Name = i.ToString();
                threads[i] = t;
                threads[i].Start();
            }
            

        }

        public static void Licz(int[,] a, int f, ref int[] kk)
        {
            int k = 0;
            string watek = Thread.CurrentThread.Name;
            int val = 0;
            Int32.TryParse(watek, out val);
            for (int i = val; i < a.GetLength(1); i++)
            {
                k += a[val, i];
            }
            kk[val] = k;
        }




    }
}
