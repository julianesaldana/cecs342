open System

[<Entrypoint>]
let rec append a b =
        match a with
        | [] -> b   // empty list appended with b is just b
        | h :: t -> h :: append t b

let rec removeFirst x coll =
    match coll with
    | [] -> []
    | h :: t when x = h -> t
    | h :: t -> h :: removeFirst x t

let rec removeAt i coll = 
    match i, coll with
    | _, [] -> failwith "cannot removeAt on an empty list"
    | 0, _::t -> t
    | _, h::t -> h :: removeAt (i - 1)  t

let main argv =
    let a = [1; 2; 3]
    let b = [4; 5; 6]
    let c = append a b
    printfn "%A" c
    0