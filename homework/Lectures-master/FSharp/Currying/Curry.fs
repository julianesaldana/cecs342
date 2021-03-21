// "Currying": a function of N>1 arguments is actually a function of 1 argument
// that returns a function of N-1 arguments.

// This LOOKS like a function of type int->int->int
let remainder x y =
    x % y

// which is a function taking 2 int parameters and returning int. But it's secretly not!

// remainder is ACTUALLY a function of a single int parameter, which returns
// a function of int->int.
// We can prove this by calling remainder with a single parameter.

let wtf = remainder 10

// What type is wtf?





// Since wtf is a function, we can invoke it
wtf 7 |> printfn "10 remainder 7 is %d"


// So what's going on when I use this function "normally"?
remainder 11 4 |> printfn "11 remainder 4 is %d"

// "remainder 11" executes first, giving a function that expects 1 argument. 4 is passed as that argument,
// and the final int is returned.

// The same as
(remainder 11) 4 |> printfn "11 remainder 4 is still %d"

// This is called currying, and it enables a technique called "partial function application".

let numbers = [2 .. 15]
numbers |> List.map (remainder 100) |> printfn "%A"

// It also explains how the pipe forward operator works.


// This is the literal definition of |>.
let (|>) v f = f v

let a = 10 |> abs // 10 is v; abs is f; we evaluate f v, i.e. abs 10

let b = 10 |> max 20 // 10 is v; max 20 is f; max 20 is a function int->int; and evaluates to 20 when applied to 10.

// F*cking awesome.