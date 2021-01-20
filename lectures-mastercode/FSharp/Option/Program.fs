// There is one union type of special importance in F#: Option<'a>
// It looks like this:
// type Option<'a> =
//     | None
//     | Some of 'a

let invalidValue = None
let validValue = Some "Neal"

// There is no "null" in F#; instead, we use Option for most use cases involving "null" in Java.

// Usage #1: a value does not exist
let lookupId = function
    | "Neal"  -> Some 1
    | "Karyl" -> Some 2
    | "Marco" -> Some 3
    | _       -> None

match lookupId "Michelle" with
| None -> printfn "Could not find that user"
| Some id -> printfn "That user has id %d" id

// Usage #2: error state
let divideInts num den =
    match den with
    | 0 -> None
    | _ -> Some (num / den)

match divideInts 10 0 with
    | None -> printfn "Could not divide those"
    | Some quotient -> printfn "The quotient was %d" quotient


// How is this different than null?
let uninit = None
// The next line fails, because the value "uninit" is None, and None is not a string 
// as expected by lookupId:

// lookupId uninit |> printfn "%d"

// which protects against passing nulls to functions that need actual values.

// If a function is ok with uninitialized values, it can take an option as a parameter:
let lookupIfValid = function
    | None -> None
    | Some n -> lookupId n