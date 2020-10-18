open System.Threading
open HttpResult

[<EntryPoint>]
let main _ =
    let writeOutput = function
        | Success text -> printfn "%s" text
        | Fail message -> printfn "%s" message
    
    let writeResponse = Async.RunSynchronously >> (HttpResult.bind HttpClient.responseToString) >> writeOutput
    let cancelToken = CancellationToken()
    HttpClient.getString "https://localhost:5001" cancelToken |> writeOutput //Get!
    HttpClient.getString "https://localhost:5001/test" cancelToken |> writeOutput //Not Found
    
    HttpClient.post "https://localhost:5001" cancelToken |> writeResponse // Post!
    HttpClient.put "https://localhost:5001" cancelToken |> writeResponse // Put!
    HttpClient.delete "https://localhost:5001" cancelToken |> writeResponse // Delete!

    0 // return an integer exit code
