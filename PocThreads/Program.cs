using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PocThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            //var before2 = GC.CollectionCount(2);
            //var before1 = GC.CollectionCount(1);
            //var before0 = GC.CollectionCount(0);
            sw.Start();


            TaskA();
            TaskB();

            //TaskThreadA();
            //TaskThreadB();

            //TaskThreadPoolA();
            //TaskThreadPoolB();

            //var t1 = TaskAsyncA();
            //var t2 = TaskAsyncB();

            //TaskParallelA();
            //TaskParallelB();

            //Task.WaitAll(t1, t2);

            sw.Stop();
            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            //Console.WriteLine($"GC Gen #2 : {GC.CollectionCount(2) - before2}");
            //Console.WriteLine($"GC Gen #1 : {GC.CollectionCount(1) - before1}");
            //Console.WriteLine($"GC Gen #0 : {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"Fim!");
            Console.ReadLine();
        }

        private static void TaskB()
        {
            ExecuteIsPrimeRageVerify(51, 100, "B");
        }



        private static void TaskA()
        {
            ExecuteIsPrimeRageVerify(1, 50, "A");
        }

        private static void TaskThreadB()
        {
            var t1 = new Thread(new ThreadStart(TaskB));
            t1.Start();

        }
        private static void TaskThreadPoolA()
        {

            ThreadPool.QueueUserWorkItem(state =>
            {
                TaskA();
            });
        }

        private static void TaskThreadPoolB()
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                TaskB();
            });
        }


        private static void TaskThreadA()
        {
            var t1 = new Thread(new ThreadStart(TaskA));
            t1.Start();
        }

        private static async Task TaskAsyncB()
        {
            await Task.Run(() =>
            {
                ExecuteIsPrimeRageVerify(51, 100, "B");
            });
        }



        private static async Task TaskAsyncA()
        {

            await Task.Run(() =>
            {
                ExecuteIsPrimeRageVerify(1, 50, "A");

            });
        }


        private static void TaskParallelB()
        {
            ExecuteIsPrimeRageVerifyParallel(51, 100, "B");
        }

        private static void TaskParallelA()
        {
            ExecuteIsPrimeRageVerifyParallel(1, 50, "A");
        }


        private static void ExecuteIsPrimeRageVerify(int init, int end, string name)
        {


            for (int i = init; i <= end; i++)
            {
                var number = i;
                var isPrime = PrimeNumber(number);
                if (isPrime)
                    Console.WriteLine($"{number} is Prime [{name}]");

            }
        }

        private static void ExecuteIsPrimeRageVerifyParallel(int init, int end, string name)
        {
            Parallel.For(init, end, number =>
            {
                var isPrime = PrimeNumber(number);
                if (isPrime)
                    Console.WriteLine($"{number} is Prime [{name}]");
            });
        }

        private static void GCMemoryAnaliys()
        {


            var collection = new List<decimal> { 10M, 20M };
            for (int i = 0; i < 1000000; i++)
            {


                //object teste = 123;
                //int teste2 = (int)teste;
                //object teste = 123;

                decimal teste = 123;
                int teste2 = (int)teste;



                //foreach (var item in collection)
                //{
                //    var result = Validar(item.ToString());
                //}

            }

        }
        public static bool PrimeNumber(int number)
        {

            if (number == 1)
                return false;

            for (double i = 2; i < number; i++)
            {
                var isDivisor = number % i == 0;
                if (isDivisor)
                    return false;

            }

            return true;

        }

        private static string Validar(string text)
        {
            return text.Replace("-", "").Replace(".", "");
        }
    }
}
