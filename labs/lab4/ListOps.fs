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

// count x coll
// count the number of values equal to x in coll.
let rec count x coll =
    match coll with
    | [] -> 0
    | h :: t -> if h = x then
                    1
                else
                    count x t


// countEvens coll
//
// count the number of even integers in coll.
let rec countEvens x coll =
    match coll with
    | [] -> 0
    | h :: t -> if x % 2 = 0 then
                    1
                else
                    countEvens x t

// lastElement coll
//
// return the last element in the list
let rec lastElement coll = 
    match coll with
    | [] -> null
    | 

// maxOverdrawn coll
//
// given a list of Accounts, return the largest Overdrawn amount, or 0 if none
// are overdrawn