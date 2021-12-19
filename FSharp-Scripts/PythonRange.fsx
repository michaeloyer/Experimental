// Recreating Python's range() function in F#

type range(start, stop, step) =
    let source =
        let stop =
            match step with
            | x when x < 0 -> stop + 1
            | x when x > 0 -> stop - 1
            | _ -> invalidArg (nameof(step)) "The step of a range cannot be zero."

        seq { start..step..stop }

    new(start, stop) = range(start, stop, 1)
    new(stop) = range(0, stop)

    interface System.Collections.Generic.IEnumerable<int> with
        member this.GetEnumerator() = this.GetEnumerator()

        member this.GetEnumerator() =
            this.GetEnumerator() :> System.Collections.IEnumerator

    interface System.IEquatable<range> with
        member this.Equals(r) = this.Equals(r)

    member _.GetEnumerator() = source.GetEnumerator()

    override _.ToString() =
        match step with
        | 1 -> sprintf $"range({start}, {stop})"
        | _ -> sprintf $"range({start}, {stop}, {step})"

    member _.start = start
    member _.stop = stop
    member _.step = step

    member _.Equals(r:range) =
        r.start = start &&
        r.stop = stop &&
        r.step = step

    override this.Equals(r) =
        match r with
        | :? range as r -> this.Equals(r)
        | _ -> false

    override _.GetHashCode() =
        System.HashCode.Combine(start, stop, step)

let printRange (r: range) =
    printfn $"{r} -> %A{(Seq.toList r)}"

range(10) |> printRange
range(1, 11) |> printRange
range(1, 21, 2) |> printRange
range(2, 21, 2) |> printRange
range(10, 1, -1) |> printRange

(range(1) = range(2)) |> printfn "%b is false"
(range(1) = range(1)) |> printfn "%b is true"

range(3).start
range(3).stop

Set [range(1);range(1)]
