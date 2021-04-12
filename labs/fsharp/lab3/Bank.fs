module Bank

type Account =
    | Balance of int
    | Overdrawn of int
    | Empty

type Password = string

type Name = string

type Customer =
    { Name: Name
      Password: Password
      Account: Account }

type Action =
    | Withdraw of int
    | Deposit of int

type Session =
    | Valid of Customer
    | BadPassword

type TransactionResult =
    | AccountUpdated of Customer
    | Failed

let makeAccount() = Empty

let withdraw amount account =
    match account with
    | Empty -> Overdrawn amount
    | Overdrawn over -> Overdrawn (abs(over - amount))
    | Balance bal when bal < amount -> Overdrawn (abs(bal - amount))
    | Balance bal when bal > amount -> Balance (bal - amount)
    | Balance bal -> Empty

let deposit amount account = 
    match account with
    | Empty -> Balance amount
    | Balance bal -> Balance (bal + amount)
    | Overdrawn over when over < amount -> Balance (amount - over)
    | Overdrawn over when over > amount -> Overdrawn (over - amount)
    | Overdrawn over -> Empty

let makeCustomer name password = 
    {Name = name; Password = password; Account = Empty}

let makeSession password customer =
    if (password <> customer.Password) then
        BadPassword
    else
        Valid customer

let performTransaction action session = 
    match session with
    | BadPassword -> Failed
    | Valid customer ->
        match action with
        | Deposit d -> AccountUpdated {Name = customer.Name; Password = customer.Password; Account = deposit d customer.Account}
        | Withdraw w -> AccountUpdated {Name = customer.Name; Password = customer.Password; Account = withdraw w customer.Account}
