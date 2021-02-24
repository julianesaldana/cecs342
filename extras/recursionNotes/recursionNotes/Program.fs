// Learn more about F# at http://fsharp.org

open System

type Account =
    | Balance of int
    | Overdrawn of int
    | Empty


// maxOverdrawn: Account list -> int
let maxOverdrawn accounts = // 'a -> 'b
    // goal is to iterate through the list of accounts and find one with the greatest values so far
    let rec maxOverdrawn' accounts acc =
        match accounts with
        | Overdrawn o :: tail ->
            if o > acc then maxOverdrawn' tail o
            else maxOverdrawn' tail acc
        | [] -> acc // if empty list (last number), return acc
        | _ :: tail -> maxOverdrawn' tail acc   // if not overdrawn, skip over

    maxOverdrawn' accounts 0


let accounts = [Balance 100; Overdrawn 100; Overdrawn 50; Overdrawn 200; Empty]
assert (200 = maxOverdrawn accounts)

[<EntryPoint>]
let main argv =

    // functional programming
    let rec listContainsRec x coll =
        match coll with
        | [] -> false
        | head :: _ when head = x -> true
        | _ :: tail -> listContainsRec x tail


    // tail recursive function
    let rec listSumRec coll =
        match coll with
        | [] -> 0
        | head :: tail -> head + listSumRec tail

   
    let countListTail x coll =

        let rec countListImp1 x coll acc =
            match coll with
            | [] -> acc
            | head :: tail when x = head -> countListImp1 x tail (acc + 1)
            | _ :: tail-> countListImp1 x tail acc

        countListImp1 x coll 0

    let reverseList coll =
        let mutable n = coll
        let mutable answer = []
        while n <> [] do
            answer <- List.head n :: answer
            n <- List.tail n
        answer


    
    // val indexOf: 'a -> 'a list -> int
    // val valueAt: int -> 'a list -> 'a
    // val countEvens: int list -> int
    // val filterEvens: int list -> int list
    0 // return an integer exit code

