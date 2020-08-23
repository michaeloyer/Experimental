[<EntryPoint>]
let main _ =
    let rules = 
        [
            (3, "Fizz")
            (5, "Buzz")
        ]

    let fizzbuzz number = 
        rules
        |> Seq.map (fun (divisor, word) -> if number % divisor = 0 then word else "")
        |> String.concat ""
        |> function
            | "" -> string number
            | value -> value

    { 1..100 } 
    |> Seq.map fizzbuzz
    |> String.concat "\n"
    |> printfn "%s"

    0
