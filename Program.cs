using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AverageMonoid
{
Test
    class Program
    {
        const int NUM_OF_CYCLE = 1000000;
        const int NUM_OF_CYCLE_TEST = 100;
        static Stopwatch _watch = new Stopwatch();
        static void Main(string[] args)
        {
            Average p1 = new Average(1);
            Average p2 = new Average(10);
            var p3 = p1.Add(p2);

            int average = p3.GetAverageValue();

            Average avgMonoid = new Average(); // Pomocí konstrukturu vytvořím "zero" hodnotu pro Average
            for (int j = 0; j < NUM_OF_CYCLE_TEST; j++)
            {
                //Classic avg
                _watch.Start();
                ComputeClassicAverage(NUM_OF_CYCLE * j + 1);
                _watch.Stop();
                long countOfMiliSecondClassicAvg = _watch.ElapsedMilliseconds;
                _watch.Reset();


                //Monoid avg
                _watch.Start();
                avgMonoid = ComputeMonoidAverage(NUM_OF_CYCLE, avgMonoid);
                _watch.Stop();                
                long countOfMiliSecondMonoidAvg = _watch.ElapsedMilliseconds;
                _watch.Reset();


                //Results
                Console.WriteLine("Number of cycle: " + j);
                if (countOfMiliSecondClassicAvg > countOfMiliSecondMonoidAvg)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("TestClassicAverage: " + countOfMiliSecondClassicAvg);
                Console.WriteLine("TestMonoidAverage: " + countOfMiliSecondMonoidAvg);

            }
        }

        private static Average ComputeMonoidAverage(int numOfCycles, Average avgMonoid)
        {
            for (int i = 0; i < numOfCycles; i++)
            {
                avgMonoid = avgMonoid.Add(new Average(i));
            }
            int result = avgMonoid.GetAverageValue();
            return avgMonoid;
        }

        private static void ComputeClassicAverage(int numOfCycles)
        {
            int value = 0;
            for (int i = 0; i < numOfCycles; i++)
            {
                value += i;
            }
            int result = value / numOfCycles;
        }
    }

    public class Average
    {
        public Average()
        {
            Total = 0;
            Count = 0;
        }

        public Average(int total)
        {
            Total = total;
            Count = 1;
        }
        public int Total { get; set; }
        public int Count { get; set; }

        public int GetAverageValue()
        {
                return Total / Count;
        }
    }

    public static class AverageExtension
    {
        public static Average Add(this Average avg1, Average avg2)
        {
            Average newAverage = new Average();
            newAverage.Count = avg1.Count + avg2.Count;
            newAverage.Total = avg1.Total + avg2.Total;
            return newAverage;
        }
    }

}
