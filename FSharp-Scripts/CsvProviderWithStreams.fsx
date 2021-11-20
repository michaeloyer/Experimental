#r "nuget:FSharp.Data"

open FSharp.Data
open System.IO
open System.Text

type t = CsvProvider<"A,B,C\n1,2,3\n4,5,6\n7,8,9">

let csvBuilder = StringBuilder("A,B,C\n")
for i in 1..5000 do
    csvBuilder.AppendLine("1,1,1") |> ignore

let stream = new MemoryStream(Encoding.UTF8.GetBytes(csvBuilder.ToString()))
let a = t.Load(stream)

for (i, row) in a.Rows |> Seq.indexed do
    if i % 1000 = 0 then
        printfn "%i: %i" i stream.Position

stream.Dispose()
