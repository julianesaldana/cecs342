// Learn more about F# at http://fsharp.org

open System

// program 1
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
    let distance = (Math.Pow((gunpowder * 30.0), 2.0) * Math.Sin(2.0 * rads)) / (2.0 * 9.81)
    printfn "Distance: %f" distance
    distance

let isHit target distance = 
    if target - distance <= 1.0 && target - distance >= -1.0 then
        printfn "\nTarget hit!\n------------------------------------------------------------------------------\n"
        0
    elif distance > target then
        printfn "\nNot hit, long\n------------------------------------------------------------------------------"
        1
    else
        printfn "\nNot hit, short\n------------------------------------------------------------------------------"
        -1

// program 2
let isPrime (num : float) = 
    let mutable result = true
    let mutable i = 2.0

    while i * i <= num && result = true do
        if num % i = 0.0 then
            result <- false
        else
            i <- i + 1.0
    result

let sumPrimes (max : float) =
    printfn "Starting...."
    let mutable sum = 0.0
    let mutable i = 3.0
    printfn "Processing"
    while i < max do
        if isPrime i then
            sum <- sum + i
        i <- i + 1.0
    sum <- sum + 2.0
    sum

[<EntryPoint>]
let main argv =
    //// program 1
    //printfn "Program 1=\n"
    //let placeTarget = placeTarget()
    //let mutable result = 1

    //while result <> 0 do
    //    printfn "Target is placed at: %f\n" placeTarget
    //    let angle = getAngle()
    //    let gunpowder = getGunpowder()
    //    let distance = calculateDistance angle gunpowder
    //    result <- isHit placeTarget distance

    // program 2
    printfn "Program 2=\n"
    printfn "Choose a number:"
    let userNum = Console.ReadLine() |> float
    printfn "\nIs %.2f a prime number? %b" userNum (isPrime 5.0)
    printfn "Sum of prime numbers up to %.2f: %.2f" userNum (sumPrimes userNum)

    0 // return an integer exit code