open System
open System.Collections.Generic
open System.Linq

let range = 5
let src = Stack<_>(Enumerable.Range(1, range).Reverse())
let ancl = Stack<_>()
let dest = Stack<_>()

let printTower name tower =
    let concatNumbers = Seq.map string >> String.concat ", "
    let towerText = tower |> concatNumbers
    printfn "%s = %s" name towerText

let printAll() =
    printTower "src " src
    printTower "dest" dest
    printTower "ancl" ancl
    printfn ""

let rec hanoi counter (src:Stack<_>) (dest:Stack<_>) (ancl:Stack<_>) =
    match counter with
    | 0 -> ()
    | _ ->
        let counter = counter - 1
        hanoi counter src ancl dest
        printAll()
        src.Pop() |> dest.Push
        hanoi counter ancl dest src

hanoi range src dest ancl

printAll()
printfn "Destination:"
printTower "dest" dest
