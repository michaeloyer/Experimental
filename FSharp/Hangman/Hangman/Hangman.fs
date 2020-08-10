module Hangman

open System

type Correct = { hitCount: int }
type Answer = { answer: string }
type Invalid = { message: string }

type Outcome =
    | Correct of Correct
    | Incorrect
    | Victory of Answer
    | Defeat of Answer
    | Invalid of Invalid

type Hangman (words, chances) =
    let random = System.Random()
    let answer = 
        words 
        |> Array.length 
        |> random.Next 
        |> Array.get words
    let status = Array.create (String.length answer) '_' 
    let mutable guesses = Set.empty
    let mutable chances = chances

    member this.Guesses with get() = guesses
    member this.Status with get() = status
    member this.Chances with get() = chances

    member this.Guess character =
        if not << Char.IsLetter <| character 
            then Invalid { message = sprintf "'%c' is an invalid character. Please enter a letter" character }
        else
        
        let character = System.Char.Parse(character.ToString().ToLower())
        if guesses.Contains(character)
            then Invalid { message = sprintf "You already guessed '%c'" character }
        else

        guesses <- guesses.Add character
        let hits = answer 
                    |> Seq.indexed 
                    |> Seq.filter (fun (_, char) -> char = character)
                    |> Seq.toArray
        hits |> Array.iter (fun (index,char) -> status.[index] <- char)

        if status |> Array.forall (fun char -> char <> '_') 
            then Victory { answer = answer }
        elif hits |> Array.tryHead |> Option.isSome 
            then Correct { hitCount = Array.length hits}
        else
            chances <- chances - 1
            if chances = 0 
                then Defeat { answer = answer }
            else 
                Incorrect
