﻿module FibEvenSum

// Calculates and returns the sum of even-valued Fibonacci numbers up to 1 million
let fibEvenSum =
    let rec fib a b =
        seq {
            yield a
            yield! fib b (a + b)
        }

    let evenFibs =
        fib 0 1
        |> Seq.takeWhile (fun x -> x <= 1000000)
        |> Seq.filter (fun x -> x % 2 = 0)

    Seq.sum evenFibs
