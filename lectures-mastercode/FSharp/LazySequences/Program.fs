// Lazy sequences and generator functions

printfn "Creating big list"
let bigList = [1 .. 100000000]
printfn "Creating big seq"
let bigSeq = seq {1 .. 100000000}


// We learned that lists are types of seqs, and while that's true, it hides
// seq's true purpose: laziness! A seq is a sequence of values where not
// every value in the sequence is necessarily "alive" at the same time.

// bigSeq is never used, so why waste time generating the sequence? Until
// a value is needed, it will never actually exist in the program.

let sumFive = bigSeq |> Seq.take 5 |> Seq.sum

// Now that we are summing elements, we actually need to know their values...
// It's at this point (Seq.sum) that we actually construct the values of
// the sequence... but which ones / how many? Well... how many do we need?

// This idea is called lazy evaluation. Don't do work until it's really necessary!
// And it extends to functions like filter and map!

let nextSum = 
    seq {1 .. 10000}
    |> Seq.take 5
    |> Seq.filter (fun x -> x % 2 = 1) 
    |> Seq.map (fun x -> pown x 2)

// Nothing here is actually generated, because no one is attempting to use 
// the result of the map. Seq.map is lazy; if you don't want its result, it won't
// request any values from its input; likewise, if you don't want Seq.filter's result,
// it won't request from its input; and Seq.take won't request from its input,
// the seq itself. Lazy!

// As usual, we aren't satisfied with viewing this as MAGIC.... so how does it work?