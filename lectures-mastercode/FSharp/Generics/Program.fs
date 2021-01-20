// Some functions in F# can work on any data type.


// This function returns its parameter.
let identity x = x

// identity works with any type of data:
printfn "%O" (identity 5) // %O prints an object, and provides no type hints at all.
printfn "%O" (identity "Hello")
printfn "%O" (identity [1; 2; 3])

// How can one function do this? Because the type of the parameter doesn't really matter;
// if we knew the type of x, we'd know the return type of identity. 

// F# implements a function like this using generics, similar to Java and other languages.
// In identity, x has a type of "'a" -- F#'s way of saying "some generic type".
// identity itself then is of type 'a -> 'a, i.e., it returns the same type as the parameter.
// When identity is called, the type of its parameter will be substituted for the generic type.

// Thus, identity 5 calls a function on int->int, identity "Hello" calls string->string, etc.

