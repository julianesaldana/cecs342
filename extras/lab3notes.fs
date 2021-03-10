open System

[<Entrypoint>]

let maxBalance name customers = 
    let filterCustomer = 
        function
        | {Name = n; Account = Balnace _} when n = name -> true
        | _ -> false

    // fold function should take a customer and the head of th elist and return the one we care about
    //
    // Current     Candidate   Winner
    // Customer -> Customer -> Customer
    let findMax current candidate = 
        match (current.Account, candidate.Account) with
        | Empty, _ -> candidate
        | Balance bCurrent, Balance bCandidate when bCurrent < bCandidate -> candidate
        | _, _ -> current
        

    List.filter filterCustomer customers // Customer list
    |> List.fold findMax unknownCustomer //'a

// List.fold <fun> <state> <coll>
// Applies <fun> to <state> and List.head <coll> -> <state>
//
// ('a -> 'b -> 'a)