module ListOps

type Account =
    | Balance of int
    | Overdrawn of int
    | Empty

// For the functions described below, first give a comment that expresses the
// type of the function, then provide an implementation that matches the
// description. (Note: make sure you understand the type of the function if your
// editor provides you with the type automatically). All of the functions below
// take lists. Make sure to note when the list can be generic.
//
// The implementation *must* be recursive.
//
// Descriptions below were provided by Mr. Neal Terrell.



// count x coll, this one can be generic
// count the number of values equal to x in coll.
let rec count x coll =
    match coll with
    | [] -> 0
    | head :: tail -> 
        if head = x then
            1 + count x tail
        else
            count x tail


// countEvens coll
// count the number of even integers in coll.
let rec countEvens coll =
    match coll with
    | [] -> 0
    | head :: tail -> 
        if head % 2 = 0 then
            1 + countEvens tail
        else
            countEvens tail

// lastElement coll, this can be generic
// return the last element in the list
let rec lastElement coll = 
    match coll with
    | [] -> failwith "Empty list"
    | [x] -> x
    | head::tail ->
        lastElement tail


// maxOverdrawn coll, this is generic type for accounts
// given a list of Accounts, return the largest Overdrawn amount, or 0 if none
// are overdrawn

let rec maxOverdrawn coll = 
    match coll with
    | [] -> 0
    | head::tail ->
        match head with
        | Overdrawn over ->
            if over > maxOverdrawn tail then
                over
            else
                maxOverdrawn tail
        | _ -> maxOverdrawn tail