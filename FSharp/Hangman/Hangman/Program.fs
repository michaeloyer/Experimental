open Hangman

[<EntryPoint>]
let main argv =
    match Hangman.Start() with
        | Vicotry -> printfn "Congratulations! You solved Hangman!"
        | Defeat -> printfn "Too bad, you failed to solve Hangman!"
    0