using Lib;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var someType = new SomeType();

            Console.WriteLine(someType.Some.IsSome());
            Console.WriteLine(someType.Some.IsNone());
            Console.WriteLine(someType.None.ValueOrDefault(-1));
            Console.WriteLine(someType.None.ValueOrDefault());
        }
    }
}