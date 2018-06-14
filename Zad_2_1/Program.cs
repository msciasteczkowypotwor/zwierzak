using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pw_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int wielkosc = 500000000;   //więcej niż 4
            int[] array = RandomArray(wielkosc);
            int a = wielkosc / 4;
            Suma suma = new Suma();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Thread t1 = new Thread(() => suma.Dodaj(Dodaj(ref array, 0, a)));
            Thread t2 = new Thread(() => suma.Dodaj(Dodaj(ref array, a, 2*a)));
            Thread t3 = new Thread(() => suma.Dodaj(Dodaj(ref array, 2*a, 3*a)));
            Thread t4 = new Thread(() => suma.Dodaj(Dodaj(ref array, 3*a, array.Length)));


            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Czas 1: " + elapsedMs);

            Console.WriteLine(suma.Dodaj(0));

            int pom=0;
            watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i=0;i< array.Length; i++)
            {
                pom += array[i];
               // Console.Write(array[i]+"   ");
            }
            elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Czas 2: " + elapsedMs);
            Console.WriteLine(pom);

           





            Console.ReadKey();
        }
        public static int[] RandomArray(int a)
        {
            int[] array = new int[a];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(0, 42);
            }

            return array;
        }
        public static int Dodaj(ref int[] c, int a, int b)
        {
            int summ = 0;
            for(int i = a; i < b; i++)
            {
                summ += c[i];
            }

            return summ;
        }


    }
    class Suma
    {
        private Object sumLock = new object();
        int Sumaa { set; get; }

        public Suma()
        {
            Sumaa = 0;
        }

        public int Dodaj(int a)
        {
            lock (sumLock)
            {
                Sumaa += a;
            }
            return Sumaa;
        }



    }
}
