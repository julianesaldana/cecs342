// Learn more about F# at http://fsharp.org

open System

let placeTarget () =
    let location = Random().NextDouble() * (float 1000)
    printfn "Placing target at: %f" location
    location
   
let getAngle () =
    printfn "Enter an angle to fire the cannon: "
    let userInput = Console.ReadLine()
    let angle = userInput |> float
    printfn "Angle is: %f" angle
    angle



[<EntryPoint>]
let main argv =
    let targetLocation = placeTarget()
    let angle = getAngle()
    0 // return an integer exit code
