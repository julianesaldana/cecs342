//Ashur Motlagh 018319910
//Julian Saldana 018462169

module ListOps

let suffixes coll =
    let rec suffixes' coll acc =
        match coll with
        | [] -> [] :: acc
        | head ::  tail ->
            suffixes' tail (coll :: acc)

    suffixes' coll [] |> List.rev
