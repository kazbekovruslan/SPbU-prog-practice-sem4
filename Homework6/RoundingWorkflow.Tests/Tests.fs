module RoundingWorkflow.Tests

open RoundingWorkflow
open NUnit.Framework
open FsUnit

[<Test>]
let ``Right computation with zero precision should return right answer`` () =
    let result =
        rounding 0 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

    result |> should equal 0

[<Test>]
let ``Right computation with positive precision should return right answer`` () =
    let result =
        rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }

    result - 0.048 |> abs |> should lessThan 0.0001

[<Test>]
let ``Computation with negative precision should throw exception`` () =
    (fun () ->
        rounding -1 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        |> ignore)
    |> should throw typeof<System.ArgumentOutOfRangeException>
