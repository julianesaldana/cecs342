module ListOps 

let reverseList coll = 
    let mutable n = coll
    let mutable answer = []
    while n <> [] do
        answer <- List.head n :: answer 
        n <- List.tail n
    answer

let reverseTail coll =
    let rec reverseImpl coll acc =
        
        match coll with 
        | [] -> acc
        | h :: t -> reverseImpl t (h :: acc) 
    reverseImpl coll []

let suffixes coll =
    let rec suffixes' temp acc =
        match coll with
        | [] -> [] :: acc
        | head :: tail ->
            suffixes' tail (coll :: acc)
    suffixes' coll []
    