// Julian Saldana 018462169
// CECS 342 SEC 11
// HW-1, DUE FEB 3 @ 10AM

open System

// program 1

let placeTarget () =
    let target = Random().NextDouble() * (float 1000)
    target
    
let getAngle () =
    let mutable angle = 100 |> float    // forced angle to be float type
    while angle >= 90.0 || angle <= 0.0 do
        printfn "Enter an angle between 0 and 90 degrees: "
        angle <- Console.ReadLine() |> float
    angle
    
let getGunpowder () =
    printfn "\nEnter a positive number for amount of gunpowder: "
    let gunpowder = Console.ReadLine() |> float
    gunpowder

let calculateDistance angle gunpowder = // takes angle and gunpowder as parameters
    printfn ("\nCalculating distance...")
    let rads = (angle * Math.PI) / 180.0
    let distance = (Math.Pow((gunpowder * 30.0), 2.0) * Math.Sin(2.0 * rads)) / (9.81)  // formula needed to calculate distance
    printfn "Distance: %f" distance
    distance

let isHit target distance = 
    if target - distance <= 1.0 && target - distance >= -1.0 then   // checking if the target and distance are within 1 meter, this takes into account both possible ways that they can be within close range
        printfn "\nTarget hit!\n------------------------------------------------------------------------------\n"
        false   // while loop will run in main until this returns false (target hit)
    elif distance > target then
        printfn "\nNot hit, long\n------------------------------------------------------------------------------"
        true
    else
        printfn "\nNot hit, short\n------------------------------------------------------------------------------"
        true


// program 2

let isPrime (num : float) = // forcing float because int would not be able to accurately contain increasingly larger numbers
    let mutable result = true
    let mutable i = 2.0

    // while loop follows condition from instructions about squared values, also makes the program run much faster as tested
    while i * i <= num && result = true do
        if num % i = 0.0 then   // if there is no remainder, not prime because it has a factor other than 1
            result <- false
        else
            i <- i + 1.0    // counter to keep going
    result  // returns boolean value

let sumPrimes (max : float) =
    let mutable sum = 0.0
    let mutable i = 3.0 // specified starting point in the instructions
    while i < max do
        if isPrime i then   // checks if prime, then adds to sum
            sum <- sum + i
        i <- i + 1.0
    sum <- sum + 2.0    // adds 2 (a prime number) to the very end since the loop doesnt account for it
    sum

[<EntryPoint>]
let main argv =
    // program 1
    printfn "Program 1=\n"
    let placeTarget = placeTarget()
    let mutable run = true

    while run do    // checks return int from function, if result = 0 (target hit) then it will stop looping
        printfn "Target is placed at: %f\n" placeTarget
        let angle = getAngle()
        let gunpowder = getGunpowder()
        let distance = calculateDistance angle gunpowder
        run <- isHit placeTarget distance    // return code will be updated

    // program 2
    printfn "Program 2=\n"
    printfn "Choose a number:"
    let userNum = Console.ReadLine() |> float
    printfn "\nIs %.2f a prime number? %b" userNum (isPrime userNum)    // formatted floats used because userNum will be a float and have lots of trailing zeroes
    printfn "Sum of prime numbers up to %.2f: %.2f" userNum (sumPrimes userNum)

    0 // return an integer exit code