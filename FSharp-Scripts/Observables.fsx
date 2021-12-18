#r "nuget:FSharp.Control.Reactive"

open FSharp.Control.Reactive
let subject = Subject.replay
let subject2 = Subject.replay

Observable.combineLatest subject subject2
|> Observable.mapi (fun i item -> i, item)
|> Observable.subscribe(printfn "%O") 


subject |> Subject.onNext 1
subject2 |> Subject.onNext 2
subject |> Subject.onNext 3
subject |> Subject.onNext 4
subject2 |> Subject.onNext 5
subject2 |> Subject.onNext 6
subject |> Subject.onNext 4
subject |> Subject.onNext 1
subject2 |> Subject.onNext 2


subject |> Observable.subscribe (printfn "%i")

let s = Subject.broadcast


s |> Observable.subscribe (printfn "sub 1: %i")
s |> Observable.subscribe (printfn "sub 2: %i")
s |> Observable.subscribe (printfn "sub 3: %i")

s |> Subject.onNext 1
s |> Subject.onNext 2
s |> Subject.onNexts [3;4;5]
