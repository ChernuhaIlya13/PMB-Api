using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private BigInteger b = new();
        static void Main()
        {
            // var sw = new Stopwatch();
            // sw.Start();
            //
            // var res = FibFast(100);
            // Console.WriteLine(sw.Elapsed);
            // Console.WriteLine(res);
            //
            // sw.Start();
            //
            // res = FibLinq(100);
            // Console.WriteLine(sw.Elapsed);
            //
            // Console.WriteLine(res);

            var t = new[] {1, 2, 3, 4};
            
            CSelect(t, i => i * i).ToList().ForEach(x => Console.Write(x + " "));
        }

        private static BigInteger FibLinq(int num) => Enumerable.Range(0, num)
            .Select(x => new BigInteger(x))
            .Aggregate((new BigInteger(1), new BigInteger(1)), 
                (tuple, _) => (tuple.Item2, tuple.Item1 + tuple.Item2)).Item2;
        
        private static BigInteger FactLinq(int num) => Enumerable.Range(1, num)
            .Select(x => new BigInteger(x))
            .Aggregate(new BigInteger(1L), BigInteger.Multiply);

        private static BigInteger FactFast(int num)
        {
            var current = new BigInteger(1);
            
            for (int i = 2; i < num + 1; i++)
            {
                current = BigInteger.Multiply(current, new BigInteger(i));
            }

            return current;
        }
        
        private static BigInteger FibFast(int num)
        {
            var prev = new BigInteger(1);
            var current = new BigInteger(1);
            
            for (var i = 3; i <= num; i++)
            {
                (prev, current) = (current, prev + current);
            }

            return current;
        }

        private static async IAsyncEnumerable<T> CSelect<T>(IEnumerable<T> list, Func<T, T> func)
        {
            foreach (var el in list)
            {
                yield return func(el);
                await Task.Delay(100);
            }
        }
    }
}