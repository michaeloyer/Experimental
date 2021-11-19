type ResultComputationExpression() =
    member _.Bind(result, func) = Result.bind func result
    member _.Return(value) = Ok value

let result = ResultComputationExpression()

let printResult = function
    | Ok n -> printfn $"Value is {n}"
    | Error message -> printfn $"ERROR!: {message}"

let getNumber expected number n = if n = expected then Ok number else Error $"expected {expected} but got {n}"
let getOne = getNumber 0 1
let getTwo = getNumber 1 2
let getThree = getNumber 2 3
let getFour = getNumber 3 4
let getFive = getNumber 4 5

// Without Computation Expression
match getOne 0 with
| Ok one ->
    match getTwo one with
    | Ok two ->
        match getThree two with
        | Ok three ->
            match getFour three with
            | Ok four ->
                match getFive four with
                | Ok five -> Ok (one + two + three + four + five)
                | Error m -> Error m
            | Error m -> Error m
        | Error m -> Error m
    | Error m -> Error m
| Error m -> Error m
|> printResult

// Computation Result
result {
    let! one = getOne 0
    let! two = getTwo one
    let! three = getThree two
    let! four = getFour three
    let! five = getFive four

    return one + two + three + four + five
}
|> printResult


// With Errors:

// Without Computation Expression
match getOne 0 with
| Ok one ->
    match getTwo one with
    | Ok two ->
        match getThree one with
        | Ok three ->
            match getFour three with
            | Ok four ->
                match getFive four with
                | Ok five -> Ok (one + two + three + four + five)
                | Error m -> Error m
            | Error m -> Error m
        | Error m -> Error m
    | Error m -> Error m
| Error m -> Error m
|> printResult

// Computation Result
result {
    let! one = getOne 0
    let! two = getTwo one
    let! three = getThree one
    let! four = getFour three
    let! five = getFive four

    return one + two + three + four + five
}
|> printResult
