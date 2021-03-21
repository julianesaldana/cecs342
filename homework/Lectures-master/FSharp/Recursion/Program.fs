// Translating iterative functions to recursion.


// Find the sum of 1+2+...+x
let findSum x = 
    let mutable sum = 0
    let mutable i = x
    while i > 0 do
        sum <- sum + i
        i <- i - 1
    sum

// Sometimes recursion is an easy translation. The sum of 1+2+...+x is just 
// x + the sum of 1+2+...+(x-1).
let rec findSumRecursive x =
    if x = 1 then 
        1
    else
        x + findSumRecursive (x - 1)


// Check if a list contains the given value.
let listContains x coll =
    let mutable n = coll
    // Keep walking until we go off the end, or find a node that matches x.
    while n <> [] && List.head n <> x do
        n <- List.tail n

    // The list contains x if we did not walk off the end.
    n <> []


// Recursion using match.
// The empty list does not contain x.
// The list of h :: t contains x if h = x, or if t contains x.
let rec listContainsRec x coll =
    match coll with
    | []                -> false
    | h :: _ when h = x -> true
    | _ :: t            -> listContainsRec x t


///////////////////////
// *PRINT* the sequence 1, 2, ..., x. 
let printSequence x =
    let mutable i = 1
    while i < x do
        printfn "%d" i
        i <- i + 1

// Sometimes the recursive function is trickier. Often, whatever used to be mutable must
// become a parameter to the recursion, so it can change between recursive calls.
let printSequenceRecursive x = 
    // If our recursion requires an additional parameter from what we want the user to provide,
    // We can define a "private" *inner* function, and then call that from this function.

    let rec printSequenceImpl limit current =
        printfn "%d" current
        if current < limit then
            printSequenceImpl limit (current + 1)

    printSequenceImpl x 1



/////////////////////// Writing tail-recursive implementations.

// Like with printSequence, a tail recursive function often needs to introduce another
// parameter to the recursion, so we use an inner function.
let findSumTail x =
    // The most useful parameter is an accumulator. It represents "the answer to the question prior to this iteration."
    // In this example, the accumulator is "the sum of the iterations prior to x; the numbers larger than x."
    let rec findSumImpl x acc = 
        if x = 0 then
            acc               // if we've counted down to 0 already, then we want the sum of the numbers larger than 0; 
                              // the accumulator.
        else                  // otherwise, recurse to the next iteration; the sum of the numbers larger than x-1 is
                              // x plus the sum of the numbers larger than x.
            findSumImpl (x - 1) (acc + x)

    // start the recursion with the "sum so far".
    findSumImpl x 0


// Count the number of instances of x in coll.
// The count of x in an empty list is 0.
// The count of x in a list of h :: t is the count of x in t, plus 1 if h = x.
let rec listCountRecursive x coll =
    match coll with
    | []                -> 0
    | h :: t when h = x -> 1 + listCountRecursive x t
    | _ :: t            -> listCountRecursive x t

let countListTail x coll =
    
    let rec countListImpl x coll acc =  // acc = "the count of the number of x's that came before this list"
        match coll with 
        | []                -> acc // if we've reached an empty list, return the count of the elements that came before this (all of them).
                                   // otherwise, recurse on the tail. The count of the elements that came before t is:
                                   //         the count of the elements that came before h, plus 1 if h = x.
        | h :: t when x = h -> countListImpl x t (acc + 1)
        | _ :: t            -> countListImpl x t acc
        

    // start the recursion with the "count so far":
    countListImpl x coll 0


// Sum the elements of an int list.
// The sum of an empty list is 0.
// The sum of a list of h :: t is h plus the sum of t.
let rec listSumRec coll =
    match coll with 
    | []            -> 0
    | h :: t        -> h + listSumRec t

let listSumTail coll =
    let rec listSumImpl coll acc = // acc = the sum of the elements that came before this list".
        match coll with 
        | [] -> acc
        | h :: t -> listSumImpl t (acc + h)

    listSumImpl coll 0


// Harder problem: reverseList

let reverseList coll =
    // We build the reverse one node at a time, by creating a new list and repeatedly pre-pending
    // each node from coll to it. 
    let mutable n = coll
    let mutable answer = []
    while n <> [] do
        // :: can be used outside of "match".
        // In this context, it means "cons": given an element x and a list y, returns a new list with head x and tail y.
        // :: can be thought of as a function with type    'a -> 'a list -> 'a list
        
        answer <- List.head n :: answer 
        n <- List.tail n
    answer

let reverseTail coll =
    let rec reverseImpl coll acc = // acc = the reverse of the elements that came before this list.
        
        match coll with 
        | [] -> acc // If we've reached the end of the list, return the reverse of the elements that came before this list (all of them).

        // Otherwise, the reverse of the list h :: t is....
        //      h followed by the reverse of the elements that came before it.
        | h :: t -> reverseImpl t (h :: acc) 
        

    reverseImpl coll []
