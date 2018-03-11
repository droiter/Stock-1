using System.IO;

namespace Heron.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] codes = File.ReadAllLines("codes.txt");

            var instance = new Stock(codes);
            instance.Process();
        }
    }
}
