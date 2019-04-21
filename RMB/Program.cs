using System;
using static RMBNumber.RMBNumber;

namespace RMB
{
    class Program
    {
        static void Main(string[] args)
        {
            var a1 = 11111111111111111111111111111M;
            var a2 = 11111111111111111121111111111M;
            var a3 = 111111111111111121111111112M;
            var a4 = 111111111111111121111111113M;
            for (var i = a1; i < a2;)
            {
                var s = ConvertToChinese(i);
                var d = ConvertToNumber(s);
                if (d != i)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(d);
                }
                i += 1M;
            }
            for (var i = a3; i < a4;)
            {
                var s = ConvertToChinese(i);
                var d = ConvertToNumber(s);
                if (d != i)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(d);
                }
                i += 0.01M;
            }

            Console.WriteLine("OK");
            Console.ReadKey();
        }

    }
}
