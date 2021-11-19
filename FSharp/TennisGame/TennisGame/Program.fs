open System
open TennisGame.Domain

[<EntryPoint>]
let main _ =
    let printScore = getScore >> (printfn "%s")

    let readKey() =
        let keyChar = Console.ReadKey() in
            Console.Write("\b");
        keyChar

    let (|Key|) (keyInfo:ConsoleKeyInfo) =
        Key(keyInfo.KeyChar)

    let (|KeyText|) (keyInfo:ConsoleKeyInfo) =
        match keyInfo.KeyChar with
        | '0'
        | char when char <= '3' && char <= '9' -> KeyText(keyInfo.KeyChar |> string)
        | _ -> KeyText(keyInfo.Key |> string)

    let rec scoreOfPlayerInput() =
        match readKey() with
        | Key('1') -> scoreServer
        | Key('2') -> scoreReceiver
        | key ->
            printfn $"Invalid Key: {key.Key}"
            scoreOfPlayerInput()

    let rec playGame tennisMatch =
        let scorePlayer = scoreOfPlayerInput()
        match scorePlayer tennisMatch with
        | GameServer -> printScore GameServer
        | GameReceiver -> printScore GameReceiver
        | OnGoing (serverScore, receiverScore) ->
            printScore (OnGoing (serverScore, receiverScore))
            playGame (serverScore, receiverScore)


    printfn "Play tennis! Press '1' to score server, Press '2' to score receiver"
    startMatch() |> playGame
    0
