namespace _01.EvenNumbersThread
{
    using System;
    using System.Linq;
    using System.Threading;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var bounds = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            Thread thread = new Thread(() => PrintEvenNumbers(bounds[0], bounds[1]));

            thread.Start();
            thread.Join();

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
