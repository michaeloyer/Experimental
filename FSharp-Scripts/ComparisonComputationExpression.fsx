open System
open System.Collections.Generic

type ComparisonBuilder() =
    member _.Yield((x: #IComparable<_>, y: #IComparable<_>)) = x.CompareTo(y)

    member _.Yield((x, y, comparer: #IComparer<_>)) = comparer.Compare(x, y)

    member _.Combine(value, getNextValue) =
        if value = 0 then
            getNextValue ()
        else
            value

    member _.Delay(f) = f

    member _.Run(f) = f ()

let comparison = ComparisonBuilder()

comparison {
    "A", "a", StringComparer.InvariantCulture
    "b", "B", StringComparer.InvariantCulture
    "C", "c", StringComparer.InvariantCulture
} // 1
|> printfn "%i"

comparison {
    "A", "a", StringComparer.InvariantCultureIgnoreCase
    "b", "B", StringComparer.InvariantCultureIgnoreCase
    "C", "c", StringComparer.InvariantCultureIgnoreCase
} // 0
|> printfn "%i"

comparison {
    1, 1
    2, 3
} // -1
|> printfn "%i"

comparison {
    1, 1
    "A", "a", StringComparer.InvariantCultureIgnoreCase
} // 0
|> printfn "%i"

// Making a type with a custom Comparison object
type Person =
    { Id: int
      First: string
      Last: string }
    static member NameComparer =
        { new IComparer<Person> with
            member _.Compare(p1, p2) =
                comparison {
                    p1.Last, p2.Last
                    p1.First, p2.First
                } }
