// A sequence is an iterable collection of homogenous values with no concern for how the values
// are represented in the sequence. 

// "collection": a bunch of data values. 
// "homogenous": all the values in the sequence are the same type.
// "iterable": we can use an Iterator to access one element in the sequence and then move
//      on to the "next" value in the sequence.

// The seq<T> type represents a sequence of values of type T, e.g., seq<int>

// A sequence can be used in a "for" loop, very similar to Python's for loop and Java's
// "enhanced for loop".
let printSequence s =
    for i in s do
        printfn "%d" i
        // What type is i?
        // Therefore what type does the sequence s contain?
        // Therefore what type is s?


// A shortcut lets us define sequences over ranges.
let longSeq = seq {1 .. 1000}
printSequence longSeq

// A sequence is not a real collection type. It cannot be added to or removed from. 
// The shortcuts we've seen involve some hacks at implementation time.
// The real purpose of seq is as a base type for other collections, including arrays
// and linked lists.



// A list is an immutable collection of homogenous data stored as a persistent linked list.
let list1 = [1; 2; 3]  // square brackets and semicolons





let list2 = 4 :: list1 // :: is "cons" ; create a new list by placing a new 
                       // head on an existing list

let list3 = list1 @ list2 // @ is "concat": concatenate/append two lists

// Lists have "properties" accessed with "." syntax, like in Java/C#. 

printfn "list1.Head: %d" list1.Head // output: 1
printfn "list2.Tail: %O" list1.Tail  // output: [2; 3]
printfn "list2's third element: %d" list1.Tail.Tail.Head  // output: 2


let getIndex (list : int list) index =
    let mutable node = list
    let mutable counter = 0
    while counter < index do
        node <- node.Tail
        counter <- counter + 1
    
    node.Head

// node is now at index 100 element























// Lists are a "generic type", in that many operations on lists are agnostic regarding
// the type of data contained in the list. We can write methods that operate on lists of any 
// type using generics.
let second (coll : 'a list) =
    coll.Tail.Head

// second is of type ('a list) -> 'a
// Given a list of some type, returns an element of the same type.
printfn "\nsecond list1: %d" (second list1)

// Writing type annotations for lists is tedious. We typically DON'T use .Head or .Tail for this reason.
// Instead, the List class has functions named "head" and "tail" that can be applied to a list object.

let third coll =
    List.head (List.tail (List.tail coll))
// coll is inferred to be of type "'a list", because List.tail takes a param of type "'a list". NICE.

// So get ".Head" and ".Tail" out of your brain. That's imperative-style thinking. Think functionally!!



// All the parens in third are making me dizzy. They are necessary to force order-of-execution, but
// they make it much harder to read the statement because the LAST function to be executed (List.head)
// comes FIRST in the statement! You're used to this as an imperative programmer, but it's a major
// point of confusion for new programmers.


// Enter the |> operator, F#'s "pipe forward" operator. |> is an infix operator that takes a value and a 
// function. The value is passed to the function as the last parameter.
let third2 coll =
    coll |> List.tail |> List.tail |> List.head

// |> can be broken over many lines for better readability
let second2 coll =
    coll
    |> List.tail
    |> List.head
// BEAUTIFUL.

// We can use |> all the time!!
second2 list1 |> printfn "second2 list: %d"



// See if a list starts with the given element.
let startsWith h coll =
    coll
    |> List.head
    |> (=) h      // all operators are also functions; but we have to put them in parens to force this.


printfn "startsWith list1 5: %O" (startsWith 1 list1)

// You know what's coming next.... recursive functions on lists!
// A recursive function includes the "rec" keyword in its declaration.
let rec contains v coll =
    if List.isEmpty coll then
        false
    elif List.head coll = v then
        true
    else
        coll
        |> List.tail
        |> contains v

 
let rec take n coll =
    if n = 0 then
        []
    else
        List.head coll :: take (n - 1) (List.tail coll)

        
// We can also match on lists
let isEmpty coll =
    match coll with
    | [] -> true
    | _  -> false
        
[1; 2; 3] |> isEmpty |> printfn "%O" // prints false
        
let rec last coll =
    match coll with
    | [x] -> x
    | h :: t -> last t
        
[1; 2; 3] |> last |> printfn "%d" // prints 3
        
        
let rec skip n coll =
    match n with
    | 0 -> coll
    | _ -> skip (n - 1) (List.tail coll)
        
[1; 2; 3; 4] |> skip 2 |> printfn "%A" // CURRYING: prints [3; 4]
        
        













