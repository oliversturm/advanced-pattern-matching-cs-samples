// In F# the keywoard `match` can be used to run an 
// explicit pattern match. 

let rec listLength l = 
    match l with 
    | [] -> 0
    | _ :: xs -> 1 + (listLength xs)

// Alternatively, this special syntax is available
// that removes the explicit parameter and the 
// `match` call.
let rec listLength' = function
    | [] -> 0
    | _ :: xs -> 1 + (listLength' xs)