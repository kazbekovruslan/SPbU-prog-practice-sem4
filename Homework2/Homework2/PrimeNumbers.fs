﻿module Homework2.PrimeNumbers

let isPrime number =
    if number < 2 then
        false
    else
        let rec checker x =
            if number % x <> 0 then
                checker (x + 1)
            else
                not (x < number)

        checker 2

let generatePrimeNumbers = Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime
