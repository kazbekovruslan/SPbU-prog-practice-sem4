module Homework2.PrimeNumbers.Test

open NUnit.Framework
open FsUnit
open Homework2.PrimeNumbers

let testCases () =
    [ 1, [ 2 ]; 4, [ 2; 3; 5; 7 ]; 10, [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ] ]
    |> List.map (fun (index, expected) -> TestCaseData(index, expected))


[<TestCaseSource(nameof (testCases))>]
let isPrimeTest index expected =
    generatePrimeNumbers |> Seq.take index |> Seq.toList |> should equal expected
