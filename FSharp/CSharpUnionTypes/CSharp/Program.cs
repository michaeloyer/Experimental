using FSharp;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDaUnion(DaUnion.NewText("Test"));
            PrintDaUnion(DaUnion.NewText2("Test2"));
            PrintDaUnion(DaUnion.NewPoint(100, 200));
            PrintDaUnion(DaUnion.NewNumber(300));
        }

        static void PrintDaUnion(DaUnion daUnion)
        {
            switch (daUnion)
            {
                case DaUnion.Text { Item: string text }:
                    Console.WriteLine($"DaUnion.Text: {text}");
                    break;

                case DaUnion.Point { Item1: int p1, Item2: int p2 }:
                    Console.WriteLine($"DaUnion.Point: P1: {p1}; P2: {p2}");
                    break;

                case DaUnion.Number { Item: int number }:
                    Console.WriteLine($"DaUnion.Number: {number}");
                    break;

                case DaUnion.Text2 { Item: string text }:
                    Console.WriteLine($"DaUnion.Text2: {text}");
                    break;
            }
        }
    }
}
