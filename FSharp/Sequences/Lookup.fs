namespace Sequences

module Map =
    let lookup key = Map.tryFind key >> Option.defaultValue Seq.empty

module Seq =
    let toLookupMap source = 
        source
        |> Seq.groupBy fst 
        |> Seq.map (fun (x,y) -> (x, Seq.map snd y))
        |> Map.ofSeq

(*

let list = [
    ("A",1)
    ("A",2)
    ("A",3)
    ("A",4)

    ("B",5)
    ("B",6)
    ("B",7)

    ("C",8)
    ("C",9)

    ("D",10)
]

printfn "F# Map"
let map = list |> Seq.toLookupMap

let printFsharpLookupKey key = map |> Map.lookup key |> Seq.map string |> String.concat ", " |> printfn "%s: %s" key
printFsharpLookupKey "A" //A: 1, 2, 3, 4
printFsharpLookupKey "B" //B: 5, 6, 7
printFsharpLookupKey "C" //C: 8, 9
printFsharpLookupKey "D" //D: 10
printFsharpLookupKey "E" //E:
    
printfn "C# Lookup"
let lookup = System.Linq.Enumerable.ToLookup(list, fst, snd)

let printCsharpLookupKey key = lookup.[key] |> Seq.map string |> String.concat ", " |> printfn "%s: %s" key
printCsharpLookupKey "A" //A: 1, 2, 3, 4
printCsharpLookupKey "B" //B: 5, 6, 7
printCsharpLookupKey "C" //C: 8, 9
printCsharpLookupKey "D" //D: 10
printCsharpLookupKey "E" //E:


*)
