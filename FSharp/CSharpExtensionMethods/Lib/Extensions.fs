namespace Lib

open System.Runtime.CompilerServices

[<Extension>]
module OptionExtensions =
    [<Extension>]
    [<CompiledName("IsSome")>]
    let isSome option = Option.isSome option

    [<Extension>]
    [<CompiledName("IsNone")>]
    let isNone option = Option.isNone option

    [<Extension>]
    [<CompiledName("ValueOrDefault")>]
    let valueOrDefaultNoDefault option =
        Option.defaultValue Unchecked.defaultof<'T> option 

    [<Extension>]
    [<CompiledName("ValueOrDefault")>]
    let valueOrDefaultWithDefault option defaultValue =
        Option.defaultValue defaultValue option
