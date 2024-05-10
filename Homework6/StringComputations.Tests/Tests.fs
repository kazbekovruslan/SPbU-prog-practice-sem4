module StringComputations.Tests

open NUnit.Framework
open FsUnit
open StringComputations

[<Test>]
let ``Computations with right data should return right answer`` () =
    let calculate = StringComputationsBuilder()

    let result =
        calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }

    match result with
    | (Ok value) -> value |> should equal 3
    | (Error msg) -> failwith "_"

[<Test>]
let ``Computations with wrong data should return error`` () =
    let calculate = StringComputationsBuilder()

    let result =
        calculate {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        }

    match result with
    | (Error msg) -> msg |> should equal "Error"
    | (Ok value) -> failwith "_"
