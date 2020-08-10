open Hangman
open System.IO
open System

[<EntryPoint>]
let main _ =
    let words = File.ReadAllLines("HangmanWords.txt");
    let chances = 10

    let hangman = Hangman(words, chances)

    let PrintStatus() = 
        let status = String.concat " " (Seq.map string hangman.Status)
        let count = Array.length hangman.Status
        let guesses = String.concat "" (Seq.map string hangman.Guesses)
        printfn ""
        printfn "%s   (%i)   Guesses: [%s]" status count guesses
        printfn "%i chances left" hangman.Chances

    let rec guess() = 
        PrintStatus()
        let userGuess = System.Console.ReadKey().KeyChar
        printfn ""

        Console.Clear()

        match hangman.Guess <| userGuess with
            | Invalid { message = message } -> 
                printfn "%s" message
                guess()
            | Correct { hitCount = count } -> 
                match count with
                    | 1 -> printfn "Correct! There is 1 '%c'" userGuess
                    | _ -> printfn "Correct! There are %i '%c'" count userGuess 
                guess()
            | Incorrect ->
                printfn "Sorry, '%c' was not found" userGuess
                guess()
            | Victory { answer = answer } -> 
                printfn "Congratulations! You solved Hangman!"
                printfn "The word was '%s'" answer
            | Defeat { answer = answer } -> 
                printfn "Too bad, you failed to solve Hangman!"
                printfn "The word was '%s'" answer

    printfn "Lets play hangman!"
    guess()
    0