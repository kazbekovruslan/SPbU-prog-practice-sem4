module Homework2.EvenNumbers.Test

open NUnit.Framework
open FsUnit
open FsCheck
open Homework2.EvenNumbers

let testCases () =
    [ [ 1; 2; 3; 4; 5 ], 2; [], 0; [ -1; -2; -3 ], 1 ]
    |> List.map (fun (list, expected) -> TestCaseData(list, expected))


[<TestCaseSource(nameof (testCases))>]
let countEvenWithMapTest list expected =
    list |> countEvenWithMap |> should equal expected

[<Test>]
let countEvenWithMapAndWithFilterEquivalent =
    Check.QuickThrowOnFailure(fun list -> countEvenWithMap list = countEvenWithFilter list)

[<Test>]
let countEvenWithMapAndWithFoldEquivalent =
    Check.QuickThrowOnFailure(fun list -> countEvenWithMap list = countEvenWithFold list)
