module Hangman

type Outcome = 
    | Vicotry
    | Defeat

type Challenge(letters: char seq) =
    let mutable letters = letters
    let mutable guess = Seq.map (fun _ -> '_') letters |> Seq.toArray
    member _.Letters with get() = letters 
    member _.Guess with get() = guess and set(value) = guess <- value
    member _.GuessLetter c = 
        let hits = letters 
                |> Seq.mapi (fun index letter -> index,letter)
                |> Seq.filter (fun t -> (snd t) = c)
        hits |> Seq.iter (fun (index,char) -> guess.[index] <- char)
        hits |> Seq.length


type Hangman(word) =
    let challenge = Challenge("test")
    let mutable chances = 5
    member this.Chances with get() = chances
    member this.Answer with get() = challenge.Letters
    member this.Solved with get() = Seq.forall (fun c -> c <> '_') challenge.Guess

    member this.Guess() =
        printfn "Enter a letter to guess (%i guesses left)" chances
        let rec UserGuess() = 
            let userGuess = System.Console.ReadKey().KeyChar 
            printfn ""
            match userGuess with
                | char when System.Char.IsLetter(char) -> System.Char.Parse(char.ToString().ToLower())
                | char -> 
                    printfn ""
                    printfn "'%c' is an invalid character. Please enter a letter" char
                    UserGuess()

        let guess = UserGuess()


        match challenge.GuessLetter guess with
            | 0 -> 
                printfn "Incorrect guess! There is no '%c'" guess
            | count -> match count with 
                        | 1 ->
                            printfn "Correct! There is 1 '%c'" guess
                        | count ->
                            printfn "Correct! There are %i '%c'" count guess 

        chances <- chances - 1 

        if this.Solved then 
            Some(Vicotry)
        elif chances = 0 then 
            Some(Defeat)
        else 
            None

    member _.ShowAnswer() = 
         String.concat " " (Seq.map string challenge.Guess) |> printfn "%s"

    static member Start() : Outcome = 
        let words = Hangman.LookupWords()
        let GetRandomWord() = 
            words |> 
            Array.length |> 
            System.Random().Next |>
            Array.get words
        
        let hangman = Hangman(GetRandomWord())
        let mutable outcome = None

        while (Option.isNone(outcome)) do 
            hangman.ShowAnswer()
            outcome <- hangman.Guess()
            printfn ""

        printfn "The word was '%s'" <| (Seq.map string >> String.concat "") hangman.Answer
        Option.get <| outcome

    static member private LookupWords() =
        System.IO.File.ReadLines("HangmanWords.txt") |> Seq.toArray;