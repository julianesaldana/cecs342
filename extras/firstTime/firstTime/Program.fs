// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    (* SECOND CLASS SESSION *)
    printfn "Hello World from F#!"

    printfn "%d is 10" 10
    let f = 10
    let pi = 3.15429

    let f2 = float f
    printfn "%f" f2

    if f = 10 then
        printfn "F is equal to 10"
    elif f = 20 then
        printfn "F is equal to 20"
    else
        printfn "F is actually equal to %d" f

    let mutable i = 0
    while i < 10 do
        printfn "%d" i
        i <- i + 1

    (* THIRD CLASS SESSION *)
    let square x = x * x

    // a function that takes an int parameter and returns int
    // int -> int

    // in arithmatic formulas, the types of all numbers must be the same, ie, float + float
    let annualInterest principal rate numYears (numYears : int) = 
        principal * ((1.0 + rate) ** (float numYears))


    // writiing a function that concatenates a string with itself
    let doubleString (s : string) =
        s + s

    // converting numbers to negative, if statements are expressions so you can do janky stuff like this
    let absoluteValue x =
        let whoa = if x < 0 then
                        -x
                    elif x = 0 then
                        0
                    else
                        x
        whoa               
    
    // int whoa = x < 0 ? -x : x == 0 ? 0 : x;
    // whoa = -x if x < 0 else 0 if x == 0 else x
    0 // return an integer exit code
