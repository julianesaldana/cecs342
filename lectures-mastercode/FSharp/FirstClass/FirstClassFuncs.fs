// It should not surprise you that functions are first class in F#.

let f = max
// f is of type int->int->int
f 10 5 |> printfn "%d"


// square is of type int->int
let square x = x * x

// isEven is of type int->bool
let isEven n = 
    n % 2 = 0

// The List type has a function called "filter" which... you guessed it! Filters a list.

let list1 = [1; 2; 3; 4; 5; 6; 7; 8; 9]

printfn "%A" (List.filter isEven list1) // %O only prints first three elements; %A prints all
// Or, with piping:
list1 
|> List.filter isEven 
|> printfn "%A"


// What is the TYPE of List.filter? Hint: combine what we know about generics and function types.





// Can we WRITE List.filter???





// We also have List.map.
list1
|> List.filter isEven
|> List.map square
|> printfn "The even values, each squared: %A"


// And fold, as List.reduce
list1
|> List.filter isEven
|> List.map square
|> List.reduce (+)
|> printfn "The sum of those values is: %A"


// Finally, how can functions be "dynamically created"? Two ways:
// As an anonymous function with the "fun" keyword
printfn "%A" (List.filter (fun x -> x % 2 = 1) list1)

list1
|> List.filter (fun x -> x % 2 = 1)
|> List.map square
|> printfn "The odd values, each squared: %A"




// An example with strings
let list2 = ["Neal"; "Mehrdad"; "Shannon"; "Josh"; "Anthony"]

// Unfortunately the next line does not compile
// printfn "%A" (List.filter (fun x -> x.Length > 4) list2)
// WHy not?


// Piping can help fix that
list2 
|> List.filter (fun x -> x.Length > 4)
|> printfn "Names longer than 4 letters: %A"



// As inner functions:
let getAdder baseVal =
    let adder x =
        baseVal + x
    adder

// Examine the type of getAdder: int -> (int -> int)
// getAdder returns a function that takes an int and returns an int...
// but how is the return value of that function calculated? It uses baseVal's value
// when getAdder is actually invoked. That is a DYNAMIC value that is not known until
// runtime, so the behavior of the resulting function won't be known until runtime. 


// To use it:
let addBy5 = getAdder 5
addBy5 10 |> printfn "5 + 10 = %d"

// Calling getAdder multiple times will result in multiple copies of "adder" being
// created, each with a different value of "baseVal".
let addBy100 = getAdder 100
addBy100 10 |> printfn "100 + 10 = %d"


// This may be troubling... how does it POSSIBLY work?







