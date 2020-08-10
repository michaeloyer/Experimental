open System
open System.Data.SQLite
open Dapper

[<CLIMutable>]
type Customer = {
    ID:int
    Name:string
}

[<EntryPoint>]
let main _ =
    let connection = new SQLiteConnection("DataSource=:memory:;Version=3")
    connection.Open() |> ignore
    connection.Execute("CREATE TABLE customer (ID INTEGER PRIMARY KEY, Name Text)") |> ignore
    connection.Execute("INSERT INTO customer (Name) VALUES ('Alice')") |> ignore
    connection.Execute("INSERT INTO customer (Name) VALUES ('Bob')") |> ignore

    connection.Query<Customer>("SELECT * FROM customer")
    |> Seq.iter (fun { ID = id; Name = name } -> printfn "ID: %i; Name: %s" id name)
    
    0 
