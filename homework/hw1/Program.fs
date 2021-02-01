// Learn more about F# at http://fsharp.org

open System

let placeTarget () =
    let target = Random().NextDouble() * (float 1000)
    target
    
let getAngle () =
    let mutable angle = 100 |> float
    while angle >= 90.0 || angle <= 0.0 do
        printfn "Enter an angle between 0 and 90 degrees: "
        angle <- Console.ReadLine() |> float
    angle
    
let getGunpowder () =
    printfn "\nEnter a positive number for amount of gunpowder: "
    let gunpowder = Console.ReadLine() |> float
    gunpowder

let calculateDistance angle gunpowder =
    printfn ("\nCalculating distance...")
    let rads = (angle * Math.PI) / 180.0
    let distance = (Math.Pow((gunpowder * 30.0), 2.0) * Math.Sin(2.0 * rads)) / 9.81
    printfn "Distance: %f" distance
    distance

let isHit target distance = 
    if target - distance <= 1.0 && target - distance >= -1.0 then
        printfn "\nTarget hit!"
        0
    elif distance > target then
        printfn "\nNot hit, long\n------------------------------------------------------------------------------"
        1
    else
        printfn "\nNot hit, short\n------------------------------------------------------------------------------"
        -1

[<EntryPoint>]
let main argv =
    let placeTarget = placeTarget()
    let mutable result = 1

    while result <> 0 do
        printfn "Target is placed at: %f" placeTarget
        let angle = getAngle()
        let gunpowder = getGunpowder()
        let distance = calculateDistance angle gunpowder
        result <- isHit 502.533 distance
    0 // return an integer exit code