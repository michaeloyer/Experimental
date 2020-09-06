namespace Lib

type SomeType() =
    member _.NoneVal: int option = None
    member _.SomeVal = Some 1