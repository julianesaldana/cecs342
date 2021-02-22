// Learn more about F# at http://fsharp.org

open System

type OptionalInt = 
    | None
    | Integer of int

[<EntryPoint>]
let main argv =
    let hasValue opt =
        match opt with
        | None -> false
        | _ -> true
    0 // return an integer exit code
