[<EntryPoint>]
let main _ =
    let rules = 
        [|
            (3, "Fizz")
            (5, "Buzz")
        |]

    let fizzbuzz number = 
        rules
        |> Array.choose (fun (divisor, word) -> if number % divisor = 0 then Some(word) else None)
        |> function
            | [||] -> string number
            | words -> String.concat "" words
    
    { 1..100 } 
    |> Seq.map fizzbuzz
    |> Seq.iter (printfn "%s")

    0