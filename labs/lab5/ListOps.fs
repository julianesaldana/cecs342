//Ashur Motlagh 018319910
//Julian Saldana 018462169

module ListOps

let reversetail coll =
    let rec reverseimpl coll acc = // acc = the reverse of the elements that came before this list.
        
        match coll with 
        | [] -> acc // if we've reached the end of the list, return the reverse of the elements that came before this list (all of them).

        // otherwise, the reverse of the list h :: t is....
        //      h followed by the reverse of the elements that came before it.
        | h :: t -> reverseimpl t (h :: acc) 
        

    reverseimpl coll []

let suffixes coll =
    let rec suffixes' coll acc =
        //let temp = (reversetail acc)
        match coll with
        | [] -> [] :: acc
        | head ::  tail ->
            suffixes' tail (coll :: acc)

    suffixes' coll [] |> List.rev
    

