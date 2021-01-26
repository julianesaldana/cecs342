// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
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
    0 // return an integer exit code
