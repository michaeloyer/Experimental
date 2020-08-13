namespace Lib

open System.Runtime.CompilerServices

[<Extension>]
type Extensions =
    [<Extension>]
    static member IsSome option = Option.isSome option

    // Alternative Implementation
    //static member IsSome option =
    //    match option with
    //    | Some _ -> true
    //    | None -> false

    [<Extension>]
    static member IsNone option = Option.isNone option

    // Alternative Implementation
    //static member IsNone option =
    //    match option with
    //    | Some _ -> false
    //    | None -> true

    [<Extension>]
    [<CompiledName("ValueOrDefault")>]
    static member ValueOrDefaultWithValue<'T> option defaultValue:'T =
        Option.defaultValue defaultValue option

    // Alternative Implementation
    //[<CompiledName("ValueOrDefault")>]
    //static member ValueOrDefault<'T> option =
    //    match option with
    //    | Some value -> value
    //    | None -> Unchecked.defaultof<'T>
        
    [<Extension>]
    [<CompiledName("ValueOrDefault")>]
    static member ValueOrDefault<'T> option =
        Extensions.ValueOrDefaultWithValue<'T> option Unchecked.defaultof<'T>

    // Alternative Implementation
    //[<CompiledName("ValueOrDefault")>]
    //static member ValueOrDefaultWithValue<'T> option defaultValue:'T =
    //    match (option, defaultValue) with
    //    | (Some value, _) -> value
    //    | (None, value) -> value