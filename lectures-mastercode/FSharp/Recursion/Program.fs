// Recursive functions, particularly with lists

// Recursive functions are defined with "let rec"

let rec listContains x coll =
    let len = List.length coll
    if len = 0 then
        false
    elif x = List.head coll then
        true
    else
        coll 
        |> List.tail
        |> listContains x

// We can do all our friendly Clojure functions in F#!
let rec listSkip n coll =
    if n = 0 then
        coll
    else
        coll
        |> List.tail
        |> listSkip (n - 1)


let rec listTake n coll =
    if n = 0 then
        []
    else
        List.head coll :: listTake (n - 1) coll
        
