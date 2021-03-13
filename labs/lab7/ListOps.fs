module ListOps

type Account =
    | Overdrawn of int
    | Balance of int
    | Empty

type Customer = { Name: string; Account: Account }

let withdraw amount acc =
    match acc with
    | Empty -> Overdrawn amount
    | Overdrawn n -> Overdrawn(amount + n)
    | Balance n when n - amount > 0 -> Balance(n - amount)
    | Balance n when n - amount < 0 -> Overdrawn(abs (n - amount))
    | Balance _ -> Empty

let deposit amount acc =
    match acc with
    | Empty -> Balance amount
    | Balance n -> Balance(amount + n)
    | Overdrawn n when n - amount > 0 -> Overdrawn(n - amount)
    | Overdrawn n when n - amount < 0 -> Balance(abs (n - amount))
    | Overdrawn _ -> Empty

let join separator listOfStrings =
    let result = listOfStrings |> List.fold (+) separator
    result
    
let simplifyBank customers names =      // taken from maxBalance function from lab6
    match customers with
    | head :: tail 