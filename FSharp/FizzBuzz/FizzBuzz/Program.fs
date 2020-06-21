open System

[<EntryPoint>]
let main argv =
    let fizzbuzz number = 
        let modWord divisor word = if number % divisor = 0 then word else ""
        match
            (modWord 3 "Fizz") +
            (modWord 5 "Buzz")
            with 
                | "" -> number.ToString()
                | value -> value

    let printFizzBuzz = printfn "%s" << fizzbuzz

    //Option 1
    for number in [1..100] do printFizzBuzz number 
    //Option 2
    Seq.iter printFizzBuzz [1..100]
    //Option 3
    [1..100] |> Seq.iter printFizzBuzz

    0
