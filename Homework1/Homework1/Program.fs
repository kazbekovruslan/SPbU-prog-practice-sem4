module Homework1

let factorial x = 
    if x < 0 then None else
    let rec recfactorial x acc =
        if x = 1 then Some(acc) else recfactorial (x - 1) (x * acc)
    recfactorial x 1

let fibonacci n = 
    let rec recfibonacci n prev cur = 
        match n with
        | 0 -> prev
        | 1 -> cur
        | _ -> recfibonacci (n - 1) cur (prev + cur)
    recfibonacci n 0 1

let convertList list =
    let rec recconvertList lst acc =
        match lst with
        | [] -> acc
        | head :: tail -> recconvertList tail (head :: acc)

    recconvertList list []

let buildPowersUpToNPlusM n m =
    if n < 0 || m < 0 then [] else
    let rec listBuilder x acc =
        if x = 0 then
            acc
        else
            listBuilder (x - 1) (List.head acc / 2 :: acc) 
    listBuilder m [pown 2 (n + m)]

let findPosition list x = 
    let rec helper l x pos =
        match l with
        | [] -> -1
        | head :: tail when head = x -> pos
        | _ -> helper (List.tail l) x (pos + 1)
    helper list x 0