using Lib;
using System;
using System.Linq.Expressions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var someType = new SomeType();

            Console.WriteLine(
$@"
{nameof(SomeType.SomeVal)}.{nameof(OptionExtensions.IsSome)}() => {someType.SomeVal.IsSome()}
{nameof(SomeType.SomeVal)}.{nameof(OptionExtensions.IsNone)}() => {someType.SomeVal.IsNone()}
{nameof(SomeType.SomeVal)}.{nameof(OptionExtensions.ValueOrDefault)}(-1) => {someType.SomeVal.ValueOrDefault(-1)}
{nameof(SomeType.SomeVal)}.{nameof(OptionExtensions.ValueOrDefault)}() => {someType.SomeVal.ValueOrDefault()}

{nameof(SomeType.NoneVal)}.{nameof(OptionExtensions.IsSome)}() => {someType.NoneVal.IsSome()}
{nameof(SomeType.NoneVal)}.{nameof(OptionExtensions.IsNone)}() => {someType.NoneVal.IsNone()}
{nameof(SomeType.NoneVal)}.{nameof(OptionExtensions.ValueOrDefault)}(-1) => {someType.NoneVal.ValueOrDefault(-1)}
{nameof(SomeType.NoneVal)}.{nameof(OptionExtensions.ValueOrDefault)}() => {someType.NoneVal.ValueOrDefault()}
");
        }
    }
}