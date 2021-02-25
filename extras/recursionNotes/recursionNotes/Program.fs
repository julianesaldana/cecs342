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
            if o > acc then maxOverdrawn' tail o    // if overdrawn greater than accumlative, set acc to overdrawn amount o, then continue with tail
            else maxOverdrawn' tail acc // else, continue with tail
        | [] -> acc // if empty list (last number), return acc
        | _ :: tail -> maxOverdrawn' tail acc   // if not overdrawn, skip over

    maxOverdrawn' accounts 0



// val indexOf: 'a -> 'a list -> int
let indexOf x coll =
    let rec indexOf' coll acc =
        match coll with
        | [] -> acc
        | head :: tail ->
            if head = x then acc
            else indexOf' tail (acc + 1)
    indexOf' coll 0 // starting function at 0



// val valueAt: int -> 'a list -> 'a
// [2; 4; 6; 8]
// what is the value at index 3? - > 8
let valueAt index coll =
    let rec valueAt' coll acc =
        match coll with
        | [] -> acc
        | head :: tail ->
            if acc = index then acc
            else valueAt' tail (acc + 1)
    valueAt' coll 0



// val countEvens: int list -> int
let countEvens coll =
    let rec countEvens' coll acc =
        match coll with
        | [] -> acc
        | head :: tail ->
            if head % 2 = 0 then countEvens' tail (acc + 1)
            else countEvens' tail acc
    countEvens' coll 0



// val filterEvens: int list -> int list
// will give you new list in reverse order
let filterEvens coll =
    let rec filterEvens' coll acc =
        match coll with
        | head :: tail when head % 2 = 0 -> filterEvens' tail (head :: acc)
        | head :: tail -> filterEvens' tail acc
        | [] -> acc

    filterEvens' coll []



let accounts = [Balance 100; Overdrawn 100; Overdrawn 50; Overdrawn 200; Empty]
assert (200 = maxOverdrawn accounts)

let xs = [2; 4; 6; 8]
assert (1 = indexOf 4 xs)



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


    
    
    0 // return an integer exit code

