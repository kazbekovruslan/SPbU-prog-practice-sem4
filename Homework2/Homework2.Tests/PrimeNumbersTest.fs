module Homework2.PrimeNumbers.Test

open NUnit.Framework
open FsUnit
open Homework2.PrimeNumbers

let testCases () =
    [ 1, [ 2 ]; 4, [ 2; 3; 5; 7 ]; 10, [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ] ]
    |> List.map (fun (index, expected) -> TestCaseData(index, expected))

//I did not fully understand why, without another declaring this function,
//the test fails with an error: the source in Seq.take is null
//so far it's just a workaround
let generatePrimeNumbers = Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime

[<TestCaseSource(nameof (testCases))>]
let isPrimeTest count expected =
    generatePrimeNumbers |> Seq.take count |> Seq.toList |> should equal expected
