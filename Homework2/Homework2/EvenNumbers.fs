module Homework2.EvenNumbers

let countEvenWithMap list = 
    list |> List.map (fun number -> if abs number % 2 = 0 then 1 else 0) |> List.sum

let countEvenWithFilter list =
    list |> List.filter (fun number -> number % 2 = 0) |> List.length 

let countEvenWithFold list = 
    list |> List.fold (fun state number -> if number % 2 = 0 then state + 1 else state) 0

printf $"{countEvenWithMap [1..5]}"