// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]

let getAdder x =
    let z  = x * 2
    fun y -> y + z  // adds z to the argument/parameter of whatever function is calling it, returns a function
                    // y is the value provided by the function when invoked

let adderFunc = getAdder 10

adderFunc 5     
|> printfn "%d\n" // prints out 25, 20 from the getAdder 10, then the function inside adds 5 (y) to 20 (z)

[1; 2; 3; 4; 5]
|> List.map adderFunc  // [41; 42; 43; 44; 45], applies the function to each integer inside the list
|> printfn "%A\n"

let main argv =
    printfn "Hello World from F#!"
    let evenOnlyList = List.filter (fun x -> x % 2 = 0) [1; 2; 3; -4; -5]
    0 // return an integer exit code
