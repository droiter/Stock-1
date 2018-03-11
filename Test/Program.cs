using System;
using System.IO;

namespace Heron.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] codes = File.ReadAllLines("codes.txt");

            DateTime start = new DateTime(2016, 5, 5);
            DateTime end = new DateTime(2016, 5, 12);

            //var instance = new Futures("IF00.CFE", start, end);
            var instance = new Stock(codes, start, end);
            instance.Process();
        }
    }
}
