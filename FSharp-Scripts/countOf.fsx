module Seq =
    let countOf predicate source =
        let mutable count = 0

        for item in source do
            if predicate item then
                count <- count + 1

        count

module Array =
    let countOf predicate source =
        let mutable i = 0
        let mutable count = 0
        let length = Array.length source

        while i < length do
            if predicate (source.[i]) then
                count <- count + 1

            i <- i + 1

module List =
    let countOf predicate source =
        let rec countOf count =
            function
            | [] -> count
            | head :: tail when predicate head -> countOf (count + 1) tail
            | _ :: tail -> countOf count tail

        countOf 0 source

    let countOfActivePattern predicate source =
        let (|Counted|NotCounted|Empty|) =
            function
            | [] -> Empty
            | head :: tail when predicate head -> Counted tail
            | _ :: tail -> NotCounted tail

        let rec countOf count =
            function
            | Empty -> count
            | Counted [] -> (count + 1)
            | NotCounted [] -> count
            | Counted tail -> countOf (count + 1) tail
            | NotCounted tail -> countOf count tail

        countOf 0 source
