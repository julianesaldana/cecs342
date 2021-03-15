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
    match coll with
    | [] -> ""
    | head :: tail -> List.fold (fun before after -> before + delim + after) head tail

let simplifyBank customers names =
    // assumption is that customers represents a single customer
    let reduceCustomer customers = 
        let combineCustomer left right =
            match right.Account with
            | Balance b -> {left with Account = deposit b left.Account}
            | Overdrawn o -> {left with Account = withdraw b left.Account}
            | Empty -> left // will choose original if dupe is empty/0
        
        List.reduce combineCustomer customers

    let simplifyCustomer acc name =
        (List.filter (fun c -> c.Name = name) customers
            |> reduceCustomer)
            :: acc

    List.fold simplifyCustomer [] names |> List.rev


// my join method WITH ERROR
// let join (separator: string) (listOfStrings: string list) =
//     listOfStrings
//     |> List.fold (fun before after -> before + separator + after) ""
    
// my simplifyBank method
// let simplifyBank (bank: Customer list) (names: string list) = 
//     let bankBalance = 
//         names |> List.map(fun x -> bank |> List.filter (fun y -> x = y.Name))
//               |> List.map(fun x -> List.fold (fun accum y -> match y.Account with
//                                                                 |Overdrawn o -> {Name = x.[0].Name ; Account = withdraw o accum.Account}
//                                                                 |Balance b -> {Name = x.[0].Name ; Account = deposit b accum.Account}
//                                                                 | _ -> {Name = x.[0].Name ; Account =deposit 0 accum.Account}) {Name = x.[0].Name ;
//     bankBalance